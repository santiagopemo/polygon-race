using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingScript : MonoBehaviour
{
    public AudioSource BgSound;
    public Slider volume;
    public Toggle effects;
    public InputField alias;
    public GameObject dataGO;
    public DontDestroy data;

    private float musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        dataGO = FindObjectOfType<DontDestroy>().gameObject;
        data = dataGO.GetComponent<DontDestroy>();
        BgSound = dataGO.GetComponent<AudioSource>();
        if (volume)
            volume.value = data.volume;
        if (effects)
            effects.isOn = data.effects;
        if (alias)
        {
            alias.characterLimit = 12;
            alias.text = data.alias;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (volume)
        {
            BgSound.volume = volume.value;
            data.volume = volume.value;
        }
        if (effects)
            data.effects = effects.isOn;
        if (alias)
            data.alias = alias.text;        
    }
}
