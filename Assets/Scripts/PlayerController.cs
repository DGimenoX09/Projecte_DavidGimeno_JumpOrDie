using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    private float horizontalInput; 
    private bool jumpInput; 
    [SerializeField]private float characterSpeed = 4.5f; 
    [SerializeField]private float jumpForce = 5f; 

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>(); 
    }

    // Start is called before the first frame update
    // despues de .a(); 
    // despues de .A =
    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce); 
    }

    // Dos == es para comparar si es igual 
    // Si es solo = es para asignar algo numero etc. 

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); 
        jumpInput = Input.GetButtonDown("Jump"); 

        if(jumpInput == true)
        {
            characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y); 
    }
}
