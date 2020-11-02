using UnityEngine;

public class SceneUtilitiesManager : MonoBehaviour
{
    public AudioSource[] sceneSounds;
    public DontDestroy data;
    // Start is called before the first frame update
    void Start()
    {
        // Get all the effects sounds in the scene and change their volume, according to the settings
        data = FindObjectOfType<DontDestroy>();
        sceneSounds = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sceneSounds.Length; i++)
        {
            if (sceneSounds[i].GetComponent<DontDestroy>() == null)
            {
                if (data.effects == false)
                    sceneSounds[i].volume = 0;
                else
                    sceneSounds[i].volume = 1;
            }
        }
    }
}
