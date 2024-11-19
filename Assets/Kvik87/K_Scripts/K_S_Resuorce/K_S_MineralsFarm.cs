using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_S_MineralsFarm : MonoBehaviour
{
    public void OnFarmMineralsPlayer()
    {
        print("вызов");
        StartCoroutine("TimeFarmMinerals");
    }
    IEnumerator TimeFarmMinerals()
    {
        yield return new WaitForSeconds(5);
        {
            print("работа таймера");
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

        }
    }
}
