using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Fundidos - Class tha handles the scene change
public class Fundidos : MonoBehaviour
{
    public Image fundido;
    // Start is called before the first frame update
    void Start()
    {
        // Hide a black image fundido slowly
        fundido.CrossFadeAlpha(0, 0.2f, false);
    }

    // When FadeOut is called shows a black image fundido for switching from one scene to another slowly
    public void FadeOut(string scene)
    {
        fundido.CrossFadeAlpha(1, 0.2f, false);
        StartCoroutine(SceneChange(scene));
        if (SceneManager.GetActiveScene().name == "Scores")
            FindObjectOfType<DontDestroy>().SaveScoresJSON();
        if (SceneManager.GetActiveScene().name == "Settings")
            FindObjectOfType<DontDestroy>().SaveSettingsJSON();
    }

    // Coroutine that changes the scene waiting certain time
    IEnumerator SceneChange(string scene)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(scene);
    }
}
