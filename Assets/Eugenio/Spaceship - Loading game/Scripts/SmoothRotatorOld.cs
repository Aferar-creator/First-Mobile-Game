using UnityEngine;

public class SmoothRotatorOld : MonoBehaviour
{
    private float currentTilt = 0f;       // Текущий угол наклона
    private float currentRotationSpeed = 0f; // Текущая скорость вращения

    [Header("Rotation Settings")]
    public float maxTiltAngle = 20f;         // Максимальный угол наклона
    public float maxRotationSpeed = 200f;   // Максимальная скорость поворота
    public float rotationAcceleration = 1f; // Ускорение вращения



    /// <summary>
    /// Обновляет угол поворота в зависимости от направления
    /// </summary>
    /// <param name="direction">Направление поворота (-1 для левого, 1 для правого, 0 для возврата)</param>
    /// <param name="deltaTime">Прошедшее время</param>
    public Quaternion UpdateRotation(float direction, float deltaTime)
    {
        // Определяем целевой угол
        float targetTilt = direction * maxTiltAngle * -1;

        // Плавно изменяем текущий угол до целевого
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, deltaTime * rotationAcceleration);

        // Плавно увеличиваем текущую скорость вращения
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, maxRotationSpeed, deltaTime * rotationAcceleration);

        // Создаем поворот
        return Quaternion.Euler(0, 0, currentTilt);
    }

    /// <summary>
    /// Сбрасывает угол поворота к исходному состоянию
    /// </summary>
    /// <param name="deltaTime">Прошедшее время</param>
    public Quaternion ResetRotation(float deltaTime)
    {
        return UpdateRotation(0, deltaTime); // Возврат к нейтральному углу
    }
}
