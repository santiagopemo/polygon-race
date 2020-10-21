using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public AudioSource AudioSource;

    private float musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = FindObjectOfType<DontDestroy>().gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource.volume = GameObject.FindGameObjectWithTag("VolumeSlider").GetComponent<Slider>().value;
    }

    public void updateVolume ( float volume )
    {
        musicVolume = volume;
    }
}
