using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    RuntimeData runtimeData = new RuntimeData();
    public static int playerScore = 0;
    public static int enemyScore  = 0;
    Text pScore;

    void Start()
    {
        pScore = GetComponent<Text> ();
      
    }
    void Update()
    {

        pScore.text = "P1 Score: " + playerScore;

    }
}
