using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultText : MonoBehaviour
{

    RuntimeData runtimeData = new RuntimeData();
    public TextMeshProUGUI rScore;
    
    
   void Update()
   {
   if (Score.playerScore>Score.enemyScore)
   {
        rScore.text = ("You Won!");
   }
   else if(Score.playerScore == Score.enemyScore)
   {
        rScore.text = ("It's a tie!");
   }
   else
   {
        rScore.text = ("You Lost!");
   }
   
   }
}
