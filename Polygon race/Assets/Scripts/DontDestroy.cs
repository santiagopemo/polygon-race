using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    public string lastScene;
    public string currentScene;

    public string alias;

    public int score;
    public Dictionary<string, int> scores;

    public float volume;
    public bool effects;
    public Dictionary<string, int> settings;

    public GameObject newPlayerPanel;
    public InputField newPlayerAlias;
    public GameObject mainMenu;
    private void Start()
    {
        score = 0;
        alias = "";
        lastScene = "";
        currentScene = SceneManager.GetActiveScene().name;
        scores = new Dictionary<string, int>();
        settings = new Dictionary<string, int>();
        setSettings();
        LoadScoresJSON();
        GetComponent<AudioSource>().volume = volume;
        newPlayerAlias.characterLimit = 12;
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
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
    public void setSettings()
    {
        int loadSettings = LoadSettingsJSON();
        if (loadSettings == 1)
        {
            volume = 1;
            //alias = "n/n";
            effects = true;
            mainMenu.SetActive(false);
            newPlayerPanel.SetActive(true);                       
        }
    }
    public void SetNewAlias()
    {
        alias = newPlayerAlias.text;
        if (alias == "")
            alias = "N/N";
        SaveSettingsJSON();
        newPlayerPanel.SetActive(false);
        mainMenu.SetActive(true);
    }
    public int SaveSettingsJSON()
    {
        SaveSystem.Init();
        string settingsJSON = "{\"volume\":" + volume + ",\"effects\":" + effects + ",\"alias\":\"" + alias + "\"}";
        SaveSystem.Save(settingsJSON, "settings.json");
        return (0);
    }

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
                // Dictionary<string, int> dictJSON = new Dictionary<string, int>();
                for (int i = 0; i < desJSONList.Length; i++)
                {
                    string[] separator2 = { "\":" };
                    string[] pair = desJSONList[i].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    if (pair.Length == 2)
                    {
                        //dictJSON.Add(pair[0], int.Parse(pair[1]));
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
                // Dictionary<string, int> dictJSON = new Dictionary<string, int>();
                for (int i = 0; i < desJSONList.Length; i++)
                {
                    string[] separator2 = { "\":" };
                    string[] pair = desJSONList[i].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                    if (pair.Length == 2)
                    {
                        //dictJSON.Add(pair[0], int.Parse(pair[1]));
                        //settings.Add(pair[0], int.Parse(pair[1]));
                        if (pair[0] == "volume")
                            volume = float.Parse(pair[1]);
                        else if (pair[0] == "effects")
                            effects = bool.Parse(pair[1]);
                        else if (pair[0] == "alias")
                        {
                            if (pair[1] == "")
                                alias = "n/n";
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
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name != currentScene)
        {
            lastScene = currentScene;
            currentScene = SceneManager.GetActiveScene().name;
            if (GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().UnPause();
        }
    }
}
