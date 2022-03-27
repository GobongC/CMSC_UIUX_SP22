using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEnemy : MonoBehaviour
{
    RuntimeData runtimeData = new RuntimeData();
    public static int enemyScore = 0;
    Text eScore;

    void Start()
    {
        eScore = GetComponent<Text>();

    }
    void Update()
    {

        eScore.text = "P2 Score: " + enemyScore;

    }
}
