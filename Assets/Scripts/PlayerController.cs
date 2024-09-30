using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    public static Animator characterAnimator; 
    private float horizontalInput; 
    private bool jumpInput; 
    private bool isAttacking; 
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

    void Movement()
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
            if(!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); 
            }
            

            characterAnimator.SetBool("IsRunning", true); 
        }
        else
        {
            characterAnimator.SetBool("IsRunning", false); 
        }
    }

    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
        characterAnimator.SetBool("IsJumping",true);
    }


    void Update()
    {
       Movement(); 

        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded && isAttacking == false) 
        {
            Jump(); 
        }

        if(Input.GetButtonDown("Attack") && GroundSensor.isGrounded && isAttacking == false)
        {
            Attack(); 
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAttacking) 
        {
            characterRigidbody.velocity = new Vector2(0, characterRigidbody.velocity.y); 
        }
        else 
        {
            characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
        }
    }

    void Attack()
    {
        StartCoroutine("AttackAnimation");
        characterAnimator.SetTrigger("Attack"); 
    }

    IEnumerator AttackAnimation() 
    {
        isAttacking = true; 

        yield return new WaitForSeconds(0.5f); 

         isAttacking = false;
    }



    void TakeDamage()
    {
        healthPoints--; 
       
        if(healthPoints <= 0) 
        {
            Die(); 
        }
        else
        {
            characterAnimator.SetTrigger("IsHurt");
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
