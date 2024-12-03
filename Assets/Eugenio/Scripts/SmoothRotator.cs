using UnityEngine;

public class SmoothRotator
{
    private float currentTilt = 0f;       // Текущий угол наклона
    private float currentRotationSpeed = 0f; // Текущая скорость вращения

    private float maxTiltAngle;          // Максимальный угол наклона
    private float maxRotationSpeed;      // Максимальная скорость вращения
    private float rotationAcceleration;  // Ускорение вращения

    public SmoothRotator(float maxTiltAngle, float maxRotationSpeed, float rotationAcceleration)
    {
        this.maxTiltAngle = maxTiltAngle;
        this.maxRotationSpeed = maxRotationSpeed;
        this.rotationAcceleration = rotationAcceleration;
    }

    /// <summary>
    /// Обновляет угол поворота в зависимости от направления.
    /// Возвращает относительное вращение.
    /// </summary>
    /// <param name="direction">Направление поворота (-1 для левого, 1 для правого, 0 для возврата).</param>
    /// <param name="deltaTime">Прошедшее время.</param>
    /// <returns>Quaternion: относительное вращение.</returns>
    public Quaternion UpdateRotation(float direction, float deltaTime)
    {
        // Определяем целевой угол
        float targetTilt = direction * maxTiltAngle;

        // Плавно изменяем текущий угол до целевого
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, deltaTime * rotationAcceleration);

        // Плавно увеличиваем текущую скорость вращения
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, maxRotationSpeed, deltaTime * rotationAcceleration);

        // Создаем относительный поворот
        return Quaternion.Euler(0, 0, currentTilt);
    }

    /// <summary>
    /// Сбрасывает угол поворота к исходному состоянию.
    /// Возвращает относительное вращение.
    /// </summary>
    /// <param name="deltaTime">Прошедшее время.</param>
    /// <returns>Quaternion: относительное вращение.</returns>
    public Quaternion ResetRotation(float deltaTime)
    {
        return UpdateRotation(0, deltaTime); // Возврат к нейтральному углу
    }
}