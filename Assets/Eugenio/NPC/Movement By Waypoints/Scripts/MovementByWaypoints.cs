using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementByWaypoints : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _waypoints = new List<Waypoint>();
    [SerializeField] private MovementToWaypoint _movementToWaypoint;
    [SerializeField] private bool _isLoop = true;

    private int _currentIndexWaypoint = 0;


    public void Move()
    {
        if (_currentIndexWaypoint < _waypoints.Count)
        {
            _movementToWaypoint.Move(_waypoints[_currentIndexWaypoint], onReachDesination: () => {
                _currentIndexWaypoint++;

                if (_isLoop && _currentIndexWaypoint >= _waypoints.Count)
                {
                    _currentIndexWaypoint = 0;
                }

                Move();
            });
        }
    }
}
