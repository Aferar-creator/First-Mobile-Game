using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_S_test_move : MonoBehaviour
{
    
   
    RaycastHit hit;
    public float dictance_hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
    private void FixedUpdate()
    {
        Ray camera_ray = new Ray();
        Debug.DrawRay(transform.position, transform.forward * dictance_hit, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, dictance_hit))
        {
            if (hit.collider.gameObject)
            {
                print(hit.collider.gameObject);
            }
        }
    }

}
