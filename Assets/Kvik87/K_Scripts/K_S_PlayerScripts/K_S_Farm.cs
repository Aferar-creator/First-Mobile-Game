using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class K_S_Farm : MonoBehaviour
{
    [Header("Добыча количества камня")]
    public int stone =0;
    int stoneQuantity;
    [Header("Время добычи камня")]
    public float timeRecoveryStone;
    float timeRecovery;
    public int wood = 0;
    public int minerals = 0;
    public Text textRosk;
    public Text textMinerals;
    public Text textWood;
    public GameObject rightHand;
    public GameObject hitGameObject;
    public K_S_EffectFarm effectFarm;

    bool stoneProduction;
    bool woodProduction;

    float time_farm = 3;
    void Start()
    {
        float timeRecovery = timeRecoveryStone;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward - new Vector3(0, transform.position.y-0.35f,0), out hit, 1f))
        {
            
            if (hit.collider.tag == "Stone")
            {
                stoneProduction = true;
            }
            if (hit.collider.tag == "woods")
            {
                woodProduction = true;
            }  
        }
        else
        {
            stoneProduction = false;
            timeRecovery = timeRecoveryStone;

            woodProduction = false;
        }
        if(stoneProduction)
        {

            OnProductionEnter("Stone", hit.collider.gameObject.GetComponent<K_S_Stone>(), hit.collider.gameObject.GetComponent<K_S_Stone>().stones.Length);
        }
        if (woodProduction)
        {
           // OnProductionEnter("woods",1);
           
        }

    }
    void OnProductionEnter(string nameProduction,K_S_Stone k_s_stone,int element)
    {       
        if (nameProduction == "Stone")
        {
          timeRecovery -=1*Time.deltaTime;

          if(timeRecovery<=0)
          {
            k_s_stone.OnFarmStonesEnter();//вызов скрипт добычи Stone (камня)
            stoneQuantity += stone;//прибавляем количество 
            textRosk.GetComponent<Text>().text = stoneQuantity.ToString();//вывод на экран UI
            timeRecovery = timeRecoveryStone;//зброс таймера
          }           
        }
        if (nameProduction == "woods")
        {
           // print("добыча дерева");
        }
       
    }
    
    void OnAnimRightHand()
    {
      
        rightHand.transform.Rotate(15, 0, 0);     

    }
}
