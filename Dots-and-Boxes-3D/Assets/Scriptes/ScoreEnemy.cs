using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreEnemy : MonoBehaviour
{
    RuntimeData runtimeData = new RuntimeData();

    public static int enemyScore = 0;

    public TextMeshProUGUI eScore;

    void Start()
    {
        eScore = GetComponent<TMPro.TextMeshProUGUI>();

    }
    void Update()
    {

        eScore.text = enemyScore.ToString();

    }
}