using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fundidos : MonoBehaviour
{
    public Image fundido;
    // Start is called before the first frame update
    void Start()
    {
        fundido.CrossFadeAlpha(0, 0.2f, false);

    }

    public void FadeOut(string scene)
    {
        fundido.CrossFadeAlpha(1, 0.2f, false);
        StartCoroutine(SceneChange(scene));
        if (SceneManager.GetActiveScene().name == "Scores")
            FindObjectOfType<DontDestroy>().SaveScoresJSON();
        if (SceneManager.GetActiveScene().name == "Settings")
            FindObjectOfType<DontDestroy>().SaveSettingsJSON();
    }

    IEnumerator SceneChange(string scene)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(scene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
