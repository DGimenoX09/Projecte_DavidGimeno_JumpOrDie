using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

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

    [SerializeField] private int maxHealth = 5; 
    [SerializeField] private int _currentHealth; 


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
        _currentHealth = maxHealth; 

        GameManager.instance.SetHeatlhBar(maxHealth);
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
        SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance.jumpAudio, 0.7f);
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
        SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance.attackAudio, 0.5f); 
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

    void Health (int health)
    {
        _currentHealth += health; 
        if(_currentHealth>maxHealth)
        {
            _currentHealth = maxHealth; 
        }
        GameManager.instance.UpdateHealtBar(_currentHealth); 
        
    }



    void TakeDamage(int damage)
    {
        _currentHealth -= damage; 
        
        GameManager.instance.UpdateHealtBar(_currentHealth); 
       
        if(_currentHealth <= 0) 
        {
            Die(); 

            SceneLoader("Game Over");
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
        SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance.DieAudio, 0.8f); 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            SceneLoader("Game Over"); 
        }
        if(collider.gameObject.layer == 8)
        {
            //characterAnimator.SetTrigger("IsHurt");
            //Destroy(gameObject, 0.45f); 
            TakeDamage(1); 
        }

        if(collider.gameObject.layer == 10)
        {
            Health(1); 
            Destroy(collider.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            TakeDamage(1); 
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(attackHitBox.position,attackRadius); 
    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }

}
