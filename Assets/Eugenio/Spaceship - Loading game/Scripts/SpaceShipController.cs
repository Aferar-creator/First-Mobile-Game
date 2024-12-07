using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Rotation Settings")]
    public float maxTiltAngle = 20f;         // Максимальный угол наклона
    public float maxRotationSpeed = 200f;   // Максимальная скорость вращения
    public float rotationAcceleration = 5f; // Ускорение вращения

    private SmoothRotator rotator;          // Экземпляр класса SmoothRotator
    private Vector3 targetPosition;

    private SpaceShipDrift drift;           // Ссылка на SpaceShipDrift

    void Start()
    {
        rotator = new SmoothRotator(maxTiltAngle, maxRotationSpeed, rotationAcceleration);
        drift = GetComponent<SpaceShipDrift>(); // Получаем SpaceShipDrift
        targetPosition = transform.position;
    }

    void Update()
    {
        HandleInput();
        Move();
        CombineRotations();
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void CombineRotations()
    {
        float direction = 0;

        if (Input.GetMouseButton(0))
        {
            direction = Input.mousePosition.x < Screen.width / 2 ? -1 : 1;
        }

        // Получаем относительное вращение от SmoothRotator
        Quaternion tiltRotation = rotator.UpdateRotation(direction, Time.deltaTime);

        // Комбинируем базовое вращение от SpaceShipDrift и наклон от SmoothRotator
        if (drift != null)
        {
            transform.rotation = drift.transform.rotation * tiltRotation;
        }
        else
        {
            transform.rotation *= tiltRotation; // Если SpaceShipDrift отсутствует
        }
    }
}
