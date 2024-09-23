using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    public static Animator characterAnimator; 
    private float horizontalInput; 
    private bool jumpInput; 
    [SerializeField]private float characterSpeed = 4.5f; 
    [SerializeField]private float jumpForce = 5f; 

    [SerializeField] private int healthPoints = 5;  

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>(); 
        characterAnimator = GetComponent<Animator>();
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

        if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); 
            characterAnimator.SetBool("IsRunning", true); 
        }
        else if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); 
            characterAnimator.SetBool("IsRunning", true); 
        }
        else
        {
            characterAnimator.SetBool("IsRunning", false); 
        }


        if(jumpInput == true && GroundSensor.isGrounded) 
        {
            characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
            characterAnimator.SetBool("IsJumping",true);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y); 
    }

    void TakeDamage()
    {
        healthPoints--; 
        characterAnimator.SetTrigger("IsHurt");

        if(healthPoints == 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        characterAnimator.SetTrigger("IsDead");
        Destroy(gameObject, 0.45f); 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 8)
        {
            //characterAnimator.SetTrigger("IsHurt");
            //Destroy(gameObject, 0.45f); 
            TakeDamage(); 
        }
    }
}
