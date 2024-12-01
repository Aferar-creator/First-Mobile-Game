using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Префаб объекта, который нужно спавнить
    public float spawnRadius = 5f;  // Радиус окружности для спавна
    public float spawnMaxHeightOffset = 5f;  // Радиус окружности для спавна
    public int numberOfObjects = 5; // Количество объектов для спавна
    public float spawnInterval = 2f; // Интервал между спавнами (в секундах)

    private void Start()
    {
        InvokeRepeating("SpawnObjects", 0f, spawnInterval);
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Генерируем случайную позицию внутри сферы
            Vector3 randomPoint = Random.insideUnitSphere * spawnRadius;
            randomPoint.y = Mathf.Clamp(randomPoint.y, -spawnRadius, spawnRadius); // Ограничиваем высоту в пределах радиуса


            Vector3 spawnPosition = transform.position + randomPoint;
            

            float randomY = Random.Range(spawnMaxHeightOffset * -1, spawnMaxHeightOffset);

            spawnPosition.z = transform.position.z;
            spawnPosition.y = transform.position.y + randomY;

            // Спавним объект
            GameObject instantiatedObject = Instantiate(objectToSpawn, spawnPosition, objectToSpawn.transform.rotation);
        }
    }
}
