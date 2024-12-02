using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed  = 20f; 
    public float maxRotationSpeed = 10f;     // Скорость поворота
    public float tiltAngle = 20f;         // Угол наклона в зависимости от направления движения
    private float currentTilt = 0f;       // Текущий наклон (для плавного изменения угла)
    private float currentRotationSpeed = 0f; // Текущая скорость вращения
    public float rotationAcceleration = 5f; // Ускорение вращения


    private Vector3 targetPosition;       // Целевая позиция объекта
    private Quaternion targetRotation;    // Целевой поворот объекта

    private void Start()
    {
        // Изначальная позиция объекта
        targetPosition = transform.position;
        targetRotation = transform.rotation;
        print("Ship is at position: " + transform.position);
    }

    private void Update()
    {
        HandleInput();
        Move();
        Rotate();
        ClampToScreen();
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Vector3 direction = Vector3.zero;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {
                // Если коснулись левой части экрана
                if (touch.position.x < Screen.width / 2)
                {
                    direction = MoveLeft();
                }
                // Если коснулись правой части экрана
                else if (touch.position.x >= Screen.width / 2)
                {
                    direction = MoveRight();
                }

                targetPosition += direction * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Возвращаемся в исходное положение при отсутствии ввода
            targetRotation = Quaternion.identity;
        }

        // // Если находимся в редакторе Unity.
        if (Application.isEditor) {
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = Vector3.zero;

                if (Input.mousePosition.x < Screen.width / 2)
                {
                    direction = MoveLeft();
                }
                else if (Input.mousePosition.x >= Screen.width / 2)
                {
                    direction = MoveRight();
                }

                targetPosition += direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                // Возвращаемся в исходное положение при отсутствии ввода
                targetRotation = Quaternion.identity;
            }
        }

        Vector3 MoveLeft()
        {
            targetRotation = Quaternion.Euler(0, 0, tiltAngle);

            return Vector3.left;
        }

        Vector3 MoveRight()
        {
            targetRotation = Quaternion.Euler(0, 0, -tiltAngle);

            return Vector3.right;
        }
    }

    private void Move()
    {
        // Плавное движение к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void Rotate()
    {
        //OLD
        // Плавный поворот к целевому углу
        // transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Плавное изменение наклона с учетом ускорения
        Quaternion targetRotation = Quaternion.Euler(0, 0, currentTilt);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, currentRotationSpeed * Time.deltaTime);

        // Постепенно увеличиваем текущую скорость вращения до максимальной
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, maxRotationSpeed, Time.deltaTime * rotationAcceleration);
    }

    private void ClampToScreen()
    {
        // Преобразуем позицию объекта в координаты экрана
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Ограничиваем координаты объекта в пределах экрана
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0f, 1f);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0f, 1f);

        // Преобразуем координаты обратно в мировые
        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);

        // Обновляем targetPosition, чтобы она тоже была в границах
        targetPosition = transform.position;
    }
}
