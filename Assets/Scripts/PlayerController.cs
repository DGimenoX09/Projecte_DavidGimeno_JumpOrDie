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
    public AudioSource audioSource; 
    [SerializeField]private float characterSpeed = 4.5f; 
    [SerializeField]private float jumpForce = 5f; 

    [SerializeField] private int healthPoints = 5; 

    [SerializeField] private Transform attackHitBox; 
    [SerializeField] private float attackRadius = 1; 


    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>(); 
        characterAnimator = GetComponent<Animator>(); 
        audioSource = GetComponent<AudioSource>(); 

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
        if(isAttacking && horizontalInput == 0)
        {
            horizontalInput = 0;
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal"); 
        }

        if(horizontalInput < 0)
        {
            if(!isAttacking)
            {
            transform.rotation = Quaternion.Euler(0, 180, 0); 
            }
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
            //Attack(); 
            StartAttack();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.Pause(); 
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
    }

    /*void Attack()
    {
        StartCoroutine("AttackAnimation");
        characterAnimator.SetTrigger("Attack"); 
        SoundManager.instance.PlaySFX(audioSource, SoundManager.instance.attackAudio, 1); 

    }

    /*IEnumerator AttackAnimation() 
    {
        isAttacking = true; 

        yield return new WaitForSeconds(0.1f); 

        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimico"))
            {
                //Destroy(enemy.gameObject); 
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>(); 
                enemyRigidbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse); 
                
                Enemy enemyScript = enemy.GetComponent<Enemy>(); 
                enemyScript.TakeDamage();


            }
        }

         yield return new WaitForSeconds(0.3f); 
        
        isAttacking = false;
    }*/

    void StartAttack()
    {
        isAttacking = true; 
        characterAnimator.SetTrigger("Attack"); 
    }


    void Attack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimico"))
            {
                //Destroy(enemy.gameObject); 
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>(); 
                enemyRigidbody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse); 
                
                Enemy enemyScript = enemy.GetComponent<Enemy>(); 
                enemyScript.TakeDamage();


            }
        }
    }


    void EndAttack() 
    {
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(attackHitBox.position,attackRadius); 
    }
}
