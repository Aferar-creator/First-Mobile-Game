using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovementWithRotationOld : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;           // Скорость движения
    [SerializeField] private SmoothRotator rotator;          // Экземпляр класса SmoothRotator

    private Vector3 targetPosition;       // Целевая позиция объекта
    private Camera mainCamera;

    void Start()
    {
        targetPosition = transform.position;
        mainCamera = Camera.main; // Получаем главную камеру
    }

    void Update()
    {
        HandleInput();
        Move();
        Rotate();
        ClampToScreen();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                // Двигаемся влево
                targetPosition += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else
            {
                // Двигаемся вправо
                targetPosition += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
    }

    private void Move()
    {
        // Плавное движение к целевой позиции
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void Rotate()
    {
        float direction = 0;

        if (Input.GetMouseButton(0))
        {
            direction = Input.mousePosition.x < Screen.width / 2 ? -1 : 1;
        }

        if (direction == 0) return;

        // Получаем новый угол поворота от SmoothRotator
        transform.rotation = rotator.UpdateRotation(direction, Time.deltaTime);
    }

    private void ClampToScreen()
    {
        // Преобразуем позицию объекта в координаты экрана
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Ограничиваем координаты объекта в пределах экрана
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0f, 1f);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0f, 1f);

        // Преобразуем координаты обратно в мировые
        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);

        // Обновляем targetPosition, чтобы она тоже была в границах
        targetPosition = transform.position;
    }
}
