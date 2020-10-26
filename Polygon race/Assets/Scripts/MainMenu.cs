using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    AudioSource mainMenuMusic;
    public void Start()
    {
        mainMenuMusic = FindObjectOfType<DontDestroy>().gameObject.GetComponent<AudioSource>();
        if (mainMenuMusic.isPlaying == false)
        {
            mainMenuMusic.Play();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
