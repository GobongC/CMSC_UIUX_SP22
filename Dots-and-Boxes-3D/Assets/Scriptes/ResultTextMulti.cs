using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResultTextMulti : MonoBehaviour
{
    
    RuntimeData runtimeData = new RuntimeData();
    public TextMeshProUGUI rScore;
    
    
   void Update()
   {
   if (Score.playerScore>Score.enemyScore)
   {
        rScore.text = ("Player One Wins!");
   }
   else if(Score.playerScore == Score.enemyScore)
   {
        rScore.text = ("It's a tie!");
   }
   else
   {
        rScore.text = ("Player Two Wins!");
   }
   
   }
}
