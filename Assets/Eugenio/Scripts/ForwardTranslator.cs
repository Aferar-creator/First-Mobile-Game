using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент для перемещения объекта вперед с случайной скоростью.
/// </summary>
[RequireComponent(typeof(Transform))]
public class ForwardTranslator : MonoBehaviour
{
    [Header("Настройки скорости")]
    [Tooltip("Минимальная скорость движения объекта.")]
    [SerializeField] private float minSpeed = 2f;

    [Tooltip("Максимальная скорость движения объекта.")]
    [SerializeField] private float maxSpeed = 10f;

    private float speed;

    /// <summary>
    /// Инициализация параметров перед началом работы.
    /// </summary>
    private void Start()
    {
        // Проверка диапазона значений скорости
        if (minSpeed > maxSpeed)
        {
            Debug.LogWarning("Минимальная скорость больше максимальной. Меняем местами значения.");
            (minSpeed, maxSpeed) = (maxSpeed, minSpeed);
        }

        // Установка случайной скорости в заданном диапазоне
        speed = Random.Range(minSpeed, maxSpeed);
    }

    /// <summary>
    /// Обновление логики движения каждый кадр.
    /// </summary>
    private void Update()
    {
        MoveObject();
    }

    /// <summary>
    /// Перемещает объект вперед на основе текущей скорости.
    /// </summary>
    private void MoveObject()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
