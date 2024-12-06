using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_S_Stone : MonoBehaviour
{
    [Header("������� ������ ��� ���������")]
    public GameObject[] stones;
    [Header("����� ������������� ��������")]
    public float timeRecovery;
    int time;//���������� �������� ������
    int element;//���������� �������� ������
    private void Start()
    {
      element = stones.Length;
      element -= 1;    
    }
  
    public void OnFarmStonesEnter()//��������� �������� ������ � ������� ����� ���������
    {
        if (time < element)
        {
            time += 1;
            OnStonesEnter(time);
        }
    }
     void OnStonesEnter(int stoneLayer)//����� ����� ������� ������� ��������� ��� ������� � ������ � �������� ���� 
    {
        for (int i = 0; i < stones.Length; i++)
        {
            stones[i].SetActive(false);//���������� ���� �������� ������ 
        }

        if (stoneLayer <= stones.Length)
        {
            stones[stoneLayer].SetActive(true);//��������� ���������� � ������
        }

        if(stones.Length==time+1)
        {
            StartCoroutine("OnTimeRecoveryEnter");//����� �������� ��������� �������� ������� ������
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    IEnumerator OnTimeRecoveryEnter()//�������
    {
       // print("����� ��������");
        yield return new WaitForSeconds(timeRecovery);

        OnStonesEnter(0);
        time = 0;
        GetComponent<CapsuleCollider>().enabled = true;
        StopCoroutine ("OnTimeRecoveryEnter");
    }    
}

   
