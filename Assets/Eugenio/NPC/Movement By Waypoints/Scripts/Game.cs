using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MovementByWaypoints _movementByWaypoints;


    private void Start()
    {
       _movementByWaypoints.Move();
    }
}
