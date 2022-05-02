using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class winlosecountermulti : MonoBehaviour
{
    RuntimeData runtimeData = new RuntimeData();
    public TextMeshProUGUI winState;

    void Update()
    {

       if(Score.playerScore>Score.enemyScore){
           winState.text = ("Player One Wins!");
       }
       else if (Score.playerScore<Score.enemyScore){
           winState.text = ("Player Two Wins!!");
       }
        else{
            winState.text = ("It's a tie!");
        }
    }
}
