using UnityEngine;
// MainMenu - Handels some behaviours of the mamin menu
public class MainMenu : MonoBehaviour
{
    AudioSource mainMenuMusic;

    // Start is called before the first frame update
    public void Start()
    {
        // Get the Background music component and plays in if is stopped 
        mainMenuMusic = FindObjectOfType<DontDestroy>().gameObject.GetComponent<AudioSource>();
        if (mainMenuMusic.isPlaying == false)
        {
            mainMenuMusic.Play();
        }
    }

    // QuitGame is called when exit option on main menu is pressed for exit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
