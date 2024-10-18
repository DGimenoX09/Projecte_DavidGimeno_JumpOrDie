using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    private int coins = 0;
    [SerializeField] Text _cointext; 
    private bool isPaused; 
    [SerializeField] GameObject _pauseCanvas; 
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
        if(!isPaused && !pauseAnimation) 
        {
            Time.timeScale = 0;
            isPaused = true;  
            _pauseCanvas.SetActive(true);
        }
        else if(isPaused && !pauseAnimation) 
        {
          pauseAnimation = true; 

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

        pauseAnimation = false; 
    }


    public void AddCoin()
    {
        coins++; 
        _cointext.text = coins.ToString(); 
    } 
    
}
