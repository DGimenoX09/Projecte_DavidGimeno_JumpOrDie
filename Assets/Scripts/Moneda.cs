using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour


{
   

    // Update is called once per frame
  
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
           GameManager.instance.AddStar(); 

            Destroy(gameObject);
        }
    }

  
}