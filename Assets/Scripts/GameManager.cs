using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    private int coins = 0;
    private int star = 0; 
    [SerializeField] Text _cointext; 
    [SerializeField] Text _startext; 

    private bool isPaused; 
    [SerializeField] GameObject _pauseCanvas; 
    [SerializeField] private Slider _healthBar; 
    private Animator _pausePanelAnimator;  
    private bool pauseAnimator; 


    void Awake()
    {
        if(instance !=null && instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            instance = this; 
        }
        _pausePanelAnimator = _pauseCanvas.GetComponentInChildren<Animator>();
    }

    public void Pause()
    {
        if(!isPaused && !pauseAnimator) 
        {
            Time.timeScale = 0;
            isPaused = true;  
            _pauseCanvas.SetActive(true);
        }
        else if(isPaused && !pauseAnimator) 
        {
          pauseAnimator = true; 

          StartCoroutine(ClosePauseAnimation()); 
        }
    }

    IEnumerator ClosePauseAnimation()
    {
        _pausePanelAnimator.SetBool("Close", true);

        yield return new WaitForSecondsRealtime(0.20f); 

        Time.timeScale = 1; 
        isPaused = false;
        _pauseCanvas.SetActive(false); 

        pauseAnimator = false; 
    }


    public void AddCoin()
    {
        coins++; 
        _cointext.text = coins.ToString(); 
        if (coins == 5)
        {
            SceneLoader("Victory"); 
        }
    } 

    public void AddStar()
    {
        star++; 
        _startext.text = star.ToString(); 
    }


    public void SetHeatlhBar(int maxHealth)
    {
        _healthBar.maxValue = maxHealth;
        _healthBar.value = maxHealth; 
    }

    public void UpdateHealtBar(int health)
    {
        _healthBar.value = health; 
    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }
    
}
