using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;        // The player's score amount
    Text text;                      // The text displaying the score amount


    void Start()
    {
        // Set up the reference
        text = GetComponent<Text>();
        // Reset the score
        score = 0;
    }


    void Update()
    {
        text.text = "Score: " + score;
    }
}