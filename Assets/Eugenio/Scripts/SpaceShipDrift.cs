using UnityEngine;

/// <summary>
/// Колебания на основе синусоидальной функции:

/// Mathf.Sin используется для создания плавных циклических движений.
/// Частота (rollFrequency, pitchFrequency, yawFrequency) определяет, как быстро меняются колебания.
/// Амплитуда (rollAmplitude, pitchAmplitude, yawAmplitude) задает, насколько сильно колеблется объект.
/// Случайные смещения (rollOffset, pitchOffset, yawOffset):

/// Смещения добавляют независимость движений по разным осям, чтобы сделать покачивание более естественным.
/// Обновление в Update:

/// На каждом кадре вычисляются новые значения углов на основе времени и применяются к объекту.
/// </summary>
public class SpaceShipDrift : MonoBehaviour
{
    [Header("Drift Settings")]
    public float rollAmplitude = 10f;      // Максимальная амплитуда вращения по оси Z (наклон)
    public float rollFrequency = 0.5f;    // Частота покачивания по оси Z
    public float pitchAmplitude = 5f;     // Максимальная амплитуда вращения по оси X (крен)
    public float pitchFrequency = 0.7f;   // Частота покачивания по оси X
    public float yawAmplitude = 3f;       // Максимальная амплитуда вращения по оси Y (рыскание)
    public float yawFrequency = 0.3f;     // Частота покачивания по оси Y

    private float rollOffset;
    private float pitchOffset;
    private float yawOffset;

    void Start()
    {
        // Генерируем случайные фазы для независимого движения
        rollOffset = Random.Range(0f, Mathf.PI * 2);
        pitchOffset = Random.Range(0f, Mathf.PI * 2);
        yawOffset = Random.Range(0f, Mathf.PI * 2);
    }

    void Update()
    {
        // Динамическое изменение амплитуд для большей случайности
        float dynamicRollAmplitude = rollAmplitude + Mathf.PerlinNoise(Time.time, 0f) * 2f;
        float dynamicPitchAmplitude = pitchAmplitude + Mathf.PerlinNoise(0f, Time.time) * 1.5f;

        // Вычисляем колебания для каждой оси
        float roll = Mathf.Sin(Time.time * rollFrequency + rollOffset) * dynamicRollAmplitude;
        float pitch = Mathf.Sin(Time.time * pitchFrequency + pitchOffset) * dynamicPitchAmplitude;
        float yaw = Mathf.Sin(Time.time * yawFrequency + yawOffset) * yawAmplitude;

        // Применяем колебания к объекту
        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
