using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class K_S_RockFarm : MonoBehaviour
{
    
    // Update is called once per frame
  
     public void OnFarmRockPlayer()
    {
        print("вызов");
        StartCoroutine("TimeFarmRock");
    }
    IEnumerator TimeFarmRock()
    {
      yield return new WaitForSeconds(5);
        {
            print("работа таймера");
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
           
        }
    }  
}
