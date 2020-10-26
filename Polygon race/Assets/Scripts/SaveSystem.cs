using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAV_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if (!Directory.Exists(SAV_FOLDER))
            Directory.CreateDirectory(SAV_FOLDER);
    }
    public static void Save(string saveString, string filename)
    {
        File.WriteAllText(SAV_FOLDER + filename, saveString);
    }

    public static string Load(string filename)
    {
        if (File.Exists(SAV_FOLDER + filename))
        {
            string savestring = File.ReadAllText(SAV_FOLDER + filename);
            return savestring;
        } else
        {
            return null;
        }

    }
}
