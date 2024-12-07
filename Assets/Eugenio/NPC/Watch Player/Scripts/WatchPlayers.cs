using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

public class WatchPlayers : MonoBehaviour
{
 /*  
 :-----------------------------------------------------------:
 :----------------    ПАРАМЕТРЫ WatchPlayers       ----------:
 :-----------------------------------------------------------:
 */
 //#######################  ОбЪекты Интерфейса.  ##################################:

 	[Header("\n")]
    [Header("SETTINGS AND PARAMETERS:  - [ WatchPlayers. ] ")]
    [Space(15)]

	[Header("Активный или нет.")]
    public bool Active = true;
    [Space(15)]

    [Header("Маска кого ищем")]
    public LayerMask TargetMask; 
    [Space(15)]

    [Header("Маска для препятствий")]
    public LayerMask ObstructionMask;
    [Space(15)]

    [Header("Угол обзора.")]
    [Range(0, 360)]
    public float FOV_F = 0.0f;
    [Space(15)]

    [Header("Дальность видимости.")]
    public float WatchDistance = 0.0f;
    [Space(15)]

    [Header("Запуск Триггера при входе.")]
    public GameObject TargetIn;// Запуск Триггера в случае, если объект вошел в зону видимости.
    [Space(15)]

    [Header("Запуск Триггера при выходе.")]
    public GameObject TargetOut;// Запуск Триггера в случае, если объект вышел из зоны видимости.
    [Space(15)]

    public bool IsTargetDetected = false;
    [Space(15)]

    [SerializeField]
    private UnityEvent onTargetIn;

    [SerializeField]
    private UnityEvent onTargetOut;

    [SerializeField]
    private MeshFilter viewMeshFilter;



    [Header("Атрибуты.")]
    public bool OnOffIndex = true;
    public Color ColorTargetIn = new Color(210f/255f, 105f/255f, 30f/255f, 1f);//Название_Цвета - chocolate.
    public Color ColorTargetOut = new Color(0f, 0f, 0f, 1f);//Название_Цвета - Black.

    private GameObject detectedTarget = null;
    public Mesh viewMesh;
    //######################  ПОЛЯ ПЕРЕМЕННЫЕ  ########################:




    //#######################  ОТРИСОВКА ЛУЧА ИНДЕКСА, НА ОБЪЕКТЫ..  ##################:

    public void OnDrawGizmos()
    {
    	if (OnOffIndex)
    	{
            if(TargetIn != null)
            {
                Gizmos.color = ColorTargetIn;//цвет линии на TargetIn.
                Gizmos.DrawLine(transform.position, TargetIn.transform.position);//рисуем линию к объекту.
            }
            if(TargetOut != null)
            {
                Gizmos.color = ColorTargetOut;//цвет линии на TargetOut.
                Gizmos.DrawLine(transform.position, TargetOut.transform.position);//рисуем линию к объекту.
            }
        }
    }

    private void Start()
    {
        StartCoroutine(FOVRoutine());

        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true) 
        {
            yield return wait;

            if (Active == false) continue;
            
            CheckInFieldOfView();
        }
    }

    private void CheckInFieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, WatchDistance, TargetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionTarget = (target.position - transform.position).normalized;
            
            if (Vector3.Angle(transform.forward, directionTarget) < FOV_F / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if ( ! Physics.Raycast(transform.position, directionTarget, distanceToTarget, ObstructionMask))
                {
                    if (detectedTarget == null)
                    {
                        onTargetIn?.Invoke();
                    }

                    IsTargetDetected = true;
                    detectedTarget = target.gameObject;
                }
                else
                {
                    OnNoTargetDetected();
                }
            }
            else
            {
                OnNoTargetDetected();
            }
        }
        else if (IsTargetDetected)
        {
            OnNoTargetDetected();
        }

        TargetIn = detectedTarget;
    }

    private void OnNoTargetDetected()
    {
        if (detectedTarget != null)
        {
            onTargetOut?.Invoke();
        }

        IsTargetDetected = false;
        detectedTarget = null;
    }
}
//End.
