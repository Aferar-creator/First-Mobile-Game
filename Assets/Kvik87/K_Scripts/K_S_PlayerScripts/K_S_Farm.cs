using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class K_S_Farm : MonoBehaviour
{
    public int rock = 0;
    public int wood = 0;
    public int minerals = 0;
    public Text textRosk;
    public Text textMinerals;
    public Text textWood;
    public GameObject rightHand;
    public GameObject hitGameObject;
    public K_S_EffectFarm effectFarm;
    
    float time_farm = 3;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {

        RaycastHit hit;
        
       // Debug.DrawRay(transform.position, transform.forward - new Vector3(0, transform.position.y - 0.35f, 0), Color.green);
        if (Physics.Raycast(transform.position, transform.forward - new Vector3(0, transform.position.y-0.35f,0), out hit, 1f))
        {
            //print("1");

           

            if (hit.collider.tag == "minerals" && hit.collider.gameObject.transform.position.y >= 0)
            {
                OnAnimRightHand();
                hit.collider.gameObject.transform.Translate(0, -0.5f * Time.deltaTime, 0);
                hitGameObject.GetComponent<ParticleSystem>().maxParticles = 10;
                if (hit.collider.gameObject.transform.position.y <= 0)
                {
                    minerals += 10;
                    textMinerals.GetComponent<Text>().text = minerals.ToString();
                    hitGameObject.GetComponent<ParticleSystem>().maxParticles = 0;
                    hit.collider.gameObject.GetComponent<K_S_MineralsFarm>().OnFarmMineralsPlayer();
                    rightHand.transform.rotation = new Quaternion(0, 0, 0, 0);
                }

            }

            if (hit.collider.tag == "rock" && hit.collider.gameObject.transform.position.y >= 0)
            {                
                OnAnimRightHand();
                hit.collider.gameObject.transform.Translate(0,-0.5f*Time.deltaTime,0);
                hitGameObject.GetComponent<ParticleSystem>().maxParticles = 10;
                if (hit.collider.gameObject.transform.position.y<=0)
                {
                    rock += 10;                   
                    textRosk.GetComponent<Text>().text = rock.ToString();
                    hitGameObject.GetComponent<ParticleSystem>().maxParticles = 0;
                    hit.collider.gameObject.GetComponent<K_S_RockFarm>().OnFarmRockPlayer();
                    rightHand.transform.rotation =new Quaternion(0,0,0,0);
                }            
            }

            if (hit.collider.tag == "woods" && hit.collider.gameObject.transform.position.y >= 0)
            {
                OnAnimRightHand();
                hit.collider.gameObject.transform.Translate(0, -0.5f * Time.deltaTime, 0);
                hitGameObject.GetComponent<ParticleSystem>().maxParticles = 10;
                if (hit.collider.gameObject.transform.position.y <= 0)
                {
                    wood += 10;
                    textWood.GetComponent<Text>().text = wood.ToString();
                    hitGameObject.GetComponent<ParticleSystem>().maxParticles = 0;
                    hit.collider.gameObject.GetComponent<K_S_WoodFarm>().OnFarmWoodPlayer();
                    rightHand.transform.rotation = new Quaternion(0, 0, 0, 0);


                }
            }
          
        }
        else
        {
            
                                
        }
        
    }
    void OnAnimRightHand()
    {
      
        rightHand.transform.Rotate(15, 0, 0);     

    }
}
