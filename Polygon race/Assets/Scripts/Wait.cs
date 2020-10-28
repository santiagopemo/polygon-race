using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Wait : MonoBehaviour
{
    public float wait_time = 10f;
    public VideoPlayer intro;

    void Start()
    {
        intro = GameObject.Find("IntroVideo").GetComponent<VideoPlayer>();
        intro.loopPointReached += OnIntroFinished;
    }

    void OnIntroFinished(VideoPlayer player)
    {
        SceneManager.LoadScene("Main menu");
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("Main menu");
    }
}
