using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MovementToWaypoint : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    private IEnumerator _movementCoroutine = null;
    private IEnumerator _lookAtWaypointCoroutine = null;


    public void Move(Waypoint waypoint, Action onReachDesination = null)
    {
        StopLookAtWaypoint();
        StopMovement();

        _movementCoroutine = MoveCoroutine(waypoint, onReachDesination);
        _lookAtWaypointCoroutine = LookAtWaypointCoroutine(waypoint);

        StartCoroutine(_movementCoroutine);
        StartCoroutine(_lookAtWaypointCoroutine);
    }

    public void StopMovement()
    {
        if (_movementCoroutine != null) 
        {
            StopCoroutine(_movementCoroutine);
            _movementCoroutine = null;
        }
    }

    public void StopLookAtWaypoint()
    {
        if (_lookAtWaypointCoroutine != null) 
        {
            StopCoroutine(_lookAtWaypointCoroutine);
            _lookAtWaypointCoroutine = null;
        }
    }


    private IEnumerator MoveCoroutine(Waypoint waypoint, Action onReachDesination = null)
    {
        while (waypoint.IsAvailable == false)
        {
            yield return null;
        }

        while (true)
        {
            Vector3 waypointPosition = waypoint.transform.position;

            Vector3 destinationPosition = new Vector3 (waypointPosition.x, transform.position.y, waypointPosition.z);

            if (Vector3.Distance(transform.position, destinationPosition) < 0.001f)
            {
                StopMovement();
                StopLookAtWaypoint();
                onReachDesination?.Invoke();
            }
            else
            {
                // Move our position a step closer to the target.
                // Calculate distance to move.
                var step =  _speed * Time.deltaTime; 
                transform.position = Vector3.MoveTowards(transform.position, destinationPosition, step);
            }

            yield return null;
        }
    }

    private IEnumerator LookAtWaypointCoroutine(Waypoint waypoint)
    {
        float degreesPerSecond = 1000;

        while (true)
        {
            if (_movementCoroutine != null)
            {
                Vector3 dirToTarget = waypoint.transform.position - transform.position;
                
                dirToTarget.y = 0;
                
                Quaternion lookRotation = Quaternion.LookRotation(dirToTarget);

                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * (degreesPerSecond/360));
                // transform.LookAt(new Vector3 (waypoint.transform.position.x, transform.position.y, waypoint.transform.position.z));
            }

            yield return null;
        }
    }
}
