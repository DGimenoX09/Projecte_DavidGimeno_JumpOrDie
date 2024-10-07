using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
   public static BGMManager instance; 

   private AudioSource _audioSource; 
   public AudioClip bgmsfx;
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

        _audioSource = GetComponent<AudioSource>();
        PlayBGM(bgmsfx);

    } 

    public void PlayBGM (AudioClip clip)
    {
        _audioSource.clip = clip; 
        _audioSource.Play(); 
    }

}
