using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

// Wait - Class that handdles the scene change when the 
// intro video is finished or the user click for skip
public class Wait : MonoBehaviour
{
    public VideoPlayer intro;
    // Start is called before the first frame update
    void Start()
    {
        intro = GameObject.Find("IntroVideo").GetComponent<VideoPlayer>();
        intro.loopPointReached += OnIntroFinished;
    }

    // OnIntroFinished is called when the intro video is finished
    void OnIntroFinished(VideoPlayer player)
    {
        SceneManager.LoadScene("Main menu");
    }

    // SkipIntro is called when the user press click anywhere in the intro scene Canvas
    public void SkipIntro()
    {
        SceneManager.LoadScene("Main menu");
    }
}
