using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_S_WoodFarm : MonoBehaviour
{
    public void OnFarmWoodPlayer()
    {
        print("�����");
        StartCoroutine("TimeFarmWood");
    }
    IEnumerator TimeFarmWood()
    {
        yield return new WaitForSeconds(5);
        {
            print("������ �������");
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

        }
    }
}

