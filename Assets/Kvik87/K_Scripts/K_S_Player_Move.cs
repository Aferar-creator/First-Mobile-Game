using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class K_S_Player_Move : MonoBehaviour
{
   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private FixedJoystick _joystick;         
   [SerializeField] private Animation _animation;

    [SerializeField] public float speedMove;
    public GameObject target;
    Vector3 move_point;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) >=1f)
        {
            transform.LookAt(target.transform.position);
            transform.position = Vector3.Lerp(transform.position,target.transform.position, Time.deltaTime);
        }
*/
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal* speedMove,_rigidbody.velocity.y,_joystick.Vertical* speedMove);
        if(_joystick.Horizontal!=0 || _joystick.Vertical!=0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    
    }
}
