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
        /*if (data.alias == "")
            txtScores.text += "Jhon Doe ........... " + data.score.ToString();
        else
            txtScores.text += data.alias + " ........... " + data.score.ToString();*/
        printScores();
    }

    void printScores()
    {
        foreach(KeyValuePair<string, int> score in data.scores.OrderByDescending(key => key.Value))
        {
            //txtScores.text += score.Key.ToString().PadRight(5, ' ') + " ............ " + score.Value.ToString().PadLeft(5, ' ') + "\n";
            txtScores.text += string.Format("{0,-14} {1,-7} {2,7}\n", score.Key, "......", score.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
