using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_S_Stone : MonoBehaviour
{
    [Header("Внесите модели для прогресса")]
    public GameObject[] stones;
    [Header("Время востановления минерала")]
    public float timeRecovery;
    int time;//количество объектов спавна
    int element;//количество объектов масива
    private void Start()
    {
      element = stones.Length;
      element -= 1;    
    }
  
    public void OnFarmStonesEnter()//сравнение объектов масива и сколько нужно выключить
    {
        if (time < element)
        {
            time += 1;
            OnStonesEnter(time);
        }
    }
     void OnStonesEnter(int stoneLayer)//вызов цикла который сначала выключает все объекты в масиве и включает один 
    {
        for (int i = 0; i < stones.Length; i++)
        {
            stones[i].SetActive(false);//выключение всех объектов масива 
        }

        if (stoneLayer <= stones.Length)
        {
            stones[stoneLayer].SetActive(true);//включение следующего в масиве
        }

        if(stones.Length==time+1)
        {
            StartCoroutine("OnTimeRecoveryEnter");//вызов счетчика включения нулевого объекта масива
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    IEnumerator OnTimeRecoveryEnter()//счетчик
    {
       // print("Вызов счетчика");
        yield return new WaitForSeconds(timeRecovery);

        OnStonesEnter(0);
        time = 0;
        GetComponent<CapsuleCollider>().enabled = true;
        StopCoroutine ("OnTimeRecoveryEnter");
    }    
}

   
