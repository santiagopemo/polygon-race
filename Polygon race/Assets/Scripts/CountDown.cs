using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// CountDown - handles the count down at the beginning of each game
public class CountDown : MonoBehaviour
{
    public Text txtReady;
    public Text txtGo;
    public GameObject car;
    AudioSource engineStartSound;

    // Start is called before the first frame update
    void Start()
    {
        car = FindObjectOfType<CarController>().gameObject;
        engineStartSound = GetComponent<AudioSource>();
        StartCoroutine(counting());
    }

    // Coroutine called when the game start
    IEnumerator counting()
    {
        // Show 'READY' text and plays the engine start sound
        txtReady.gameObject.SetActive(true);
        this.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3);

        // Show 'GO' text, hide 'READY' text, and starts the player's car
        txtReady.gameObject.SetActive(false);
        txtGo.gameObject.SetActive(true);
        car.GetComponent<AudioSource>().Play();
        car.GetComponent<CarController>().startCar = true;
        yield return new WaitForSeconds(2);

        //hide 'GO' text
        txtGo.gameObject.SetActive(false);
    }
}
