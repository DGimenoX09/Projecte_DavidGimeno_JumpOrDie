using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int vidasPoints = 3; 
    public int currentvidas; 
    // Start is called before the first frame update
    // int == igual a numero entero 
    // float == igual a numnero con decimales 
    void Start()
    {
        currentvidas = vidasPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentvidas--; 
        //currentvidas--; quita vida de uno en uno 
        //currentvidas -= 2; el -= es para restar de dos en dos o mas

    if(currentvidas <= 0) 
    {
       Die(); 
    }

    }

    void Die()
    {
        Destroy(gameObject); 
    }


}
