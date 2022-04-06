using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    RuntimeData runtimeData = new RuntimeData();

    public static int playerScore = 0;
    public static int enemyScore  = 0;
    public TextMeshProUGUI pScore;

    void Start()
    {
        pScore = GetComponent<TMPro.TextMeshProUGUI>();

    }
    void Update()
    {

        pScore.text = playerScore.ToString();

    }
}