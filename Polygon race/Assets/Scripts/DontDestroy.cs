using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

// DontDestroy - Class that handels de behaviors of a object that nerver get destroy with scenes change
public class DontDestroy : MonoBehaviour
{
    public string lastScene;
    public string currentScene;

    public string alias;

    public int score;
    public Dictionary<string, int> scores;

    public float volume;
    public bool effects;

    public GameObject newPlayerPanel;
    public InputField newPlayerAlias;
    public GameObject mainMenu;

    // Start is called before the first frame update
    private void Start()
    {
        score = 0;
        alias = "";
        lastScene = "";
        currentScene = SceneManager.GetActiveScene().name;
        scores = new Dictionary<string, int>();
        setSettings();
        LoadScoresJSON();
        GetComponent<AudioSource>().volume = volume;
        newPlayerAlias.characterLimit = 12;
    }

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // Prevents an object with this behavior from being destroyed with the scene change
        DontDestroyOnLoad(this.gameObject);
    }

    // Saves the score in the scores dictionary under the current alias
    public void saveInScores(int score)
    {
        if (alias == "")
            alias = "n/n";
        if (scores.ContainsKey(alias))
        {
            if (score <= scores[alias])
                return;
            else
                scores[alias] = score;
        }
        else
            scores.Add(alias, score);
    }

    // Set the initial settings
    public void setSettings()
    {
        int loadSettings = LoadSettingsJSON();
        if (loadSettings == 1)
        {
            volume = 1;
            effects = true;
            mainMenu.SetActive(false);
            newPlayerPanel.SetActive(true);                       
        }
    }

    // Set the alias for a new user that had not runs the application yet
    public void SetNewAlias()
    {
        alias = newPlayerAlias.text;
        if (alias == "")
            alias = "N/N";
        SaveSettingsJSON();
        newPlayerPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Serializates and saves the current settings int to settings.json file
    public int SaveSettingsJSON()
    {
        SaveSystem.Init();
        string settingsJSON = "{\"volume\":" + volume + ",\"effects\":" + effects + ",\"alias\":\"" + alias + "\"}";
        SaveSystem.Save(settingsJSON, "settings.json");
        return (0);
    }

    // Serializates and saves the scores dictionary int to scores.json file
    public int SaveScoresJSON()
    {
        SaveSystem.Init();
        string scoresJSON = "{";
        foreach (KeyValuePair<string, int> score in scores)
        {
            scoresJSON += "\"" + score.Key + "\": " + score.Value + ","; 
        }
        if (scoresJSON[scoresJSON.Length - 1] == ',')
            scoresJSON = scoresJSON.Remove(scoresJSON.Length - 1);
        scoresJSON += '}';
        SaveSystem.Save(scoresJSON, "scores.json");
        return (0);
    }

    // Deserializates scores.json file and save the scores in the scores dictionary
    public int LoadScoresJSON()
    {
        string savestring = SaveSystem.Load("scores.json");
        if (savestring != null)
        {
            if (savestring != "{}" && savestring.Length > 5)
            {
                string desJSON = savestring.Substring(2, savestring.Length - 3);
                string[] separator = { ",\"" };
                string[] desJSONList = desJSON.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < desJSONList.Length; i++)
                {
                    string[] separator2 = { "\":" };
                    string[] pair = desJSONList[i].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    if (pair.Length == 2)
                    {
                        scores.Add(pair[0], int.Parse(pair[1]));
                    }
                    else
                        return (1);
                }
                return (0);
            }
            else
                return (1);
        } else
        {
            return (1);
        }
        
    }

    // Deserializates settings.json file and save the settings in each corresponding variable
    public int LoadSettingsJSON()
    {
        string savestring = SaveSystem.Load("settings.json");
        if (savestring != null)
        {
            if (savestring != "{}" && savestring.Length > 5)
            {
                string desJSON = savestring.Substring(2, savestring.Length - 3);
                string[] separator = { ",\"" };
                string[] desJSONList = desJSON.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < desJSONList.Length; i++)
                {
                    string[] separator2 = { "\":" };
                    string[] pair = desJSONList[i].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    if (pair.Length == 2)
                    {
                        if (pair[0] == "volume")
                            volume = float.Parse(pair[1]);
                        else if (pair[0] == "effects")
                            effects = bool.Parse(pair[1]);
                        else if (pair[0] == "alias")
                        {
                            if (pair[1] == "")
                                alias = "N/N";
                            else if (pair[1].Length > 2)
                                alias = pair[1].Substring(1, pair[1].Length - 2);
                            else
                                return (1);
                        }                           
                        else
                            return (1);
                    }
                    else
                        return (1);
                }
                return (0);
            }
            else
                return (1);
        }
        else
        {
            return (1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // If the scene change
        if (SceneManager.GetActiveScene().name != currentScene)
        {
            lastScene = currentScene;
            currentScene = SceneManager.GetActiveScene().name;
            if (GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().UnPause();
        }
    }
}
