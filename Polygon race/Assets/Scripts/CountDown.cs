using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text txtReady;
    public Text txtGo;
    public GameObject car;
    AudioSource engineStartSound;

    void Start()
    {
        car = FindObjectOfType<CarController>().gameObject;
        engineStartSound = GetComponent<AudioSource>();
        StartCoroutine(counting());
    }

    IEnumerator counting()
    {
        txtReady.gameObject.SetActive(true);
        this.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3);

        txtReady.gameObject.SetActive(false);
        txtGo.gameObject.SetActive(true);
        car.GetComponent<AudioSource>().Play();
        car.GetComponent<CarController>().startCar = true;
        yield return new WaitForSeconds(2);

        txtGo.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
