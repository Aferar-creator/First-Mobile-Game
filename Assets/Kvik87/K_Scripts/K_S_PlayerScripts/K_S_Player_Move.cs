using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class K_S_Player_Move : MonoBehaviour
{
   [Header("Прафаб джойстика")]
   public FloatingJoystick _floatingJoystick;
   [Header("Скорость передвижения")]
   public float speedMove;
  
   [Header("Скорость поворота")]
    public float speedRotation;

   [Header("Привязка камеры к игроку")]
   public GameObject target;
   Rigidbody _rigidbody;
   CharacterController _characterController;
    private void Start()
    {
        if(GetComponent<Rigidbody>()==null)
        {
          gameObject.AddComponent<Rigidbody>();
          GetComponent<Rigidbody>().freezeRotation = true;        
        }
        if(GetComponent<CharacterController>()==null)
        {
            gameObject.AddComponent<CharacterController>();
            _characterController = GetComponent<CharacterController>();
        }                
    }
    private void FixedUpdate()
    {
        //движение плеера
        var movementDerection = new Vector3(_floatingJoystick.Direction.x, 0f, _floatingJoystick.Direction.y);
        _characterController.SimpleMove(movementDerection * speedMove);

        //поворот плеера
        var targetDerection = Vector3.RotateTowards(_characterController.transform.forward, movementDerection, speedRotation * Time.deltaTime, 0.0f);
        _characterController.transform.rotation = Quaternion.LookRotation(targetDerection);       
      
        if (target.transform.position.x != transform.position.x|| target.transform.position.z != transform.position.z)
        {
            //жвижение камеры за игроком 
            target.transform.position = Vector3.Lerp(new Vector3(target.transform.position.x,1,target.transform.position.z),new Vector3(transform.position.x,0,transform.position.z),Time.deltaTime*5);
        }
    }
}
