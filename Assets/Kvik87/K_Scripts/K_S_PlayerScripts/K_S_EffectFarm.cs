using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_S_EffectFarm : MonoBehaviour
{
    public void OnEffectEnter()
    {
        print("�������� ������");
      gameObject.GetComponent<ParticleSystem>().maxParticles = 10;   
    }
    public void OnEffectExit()
    {
        print("��������� ������");
        gameObject.GetComponent<ParticleSystem>().maxParticles = 0;      
    }
}
