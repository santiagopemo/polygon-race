using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoresController : MonoBehaviour
{
    public DontDestroy data;
    public Text txtScores;
    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<DontDestroy>();
        printScores();
    }

    // Print the scores save in scores dictionary
    void printScores()
    {
        foreach(KeyValuePair<string, int> score in data.scores.OrderByDescending(key => key.Value))
        {
            txtScores.text += string.Format("{0,-14} {1,-7} {2,7}\n", score.Key, "......", score.Value);
        }
    }
}
