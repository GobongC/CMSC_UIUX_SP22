using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class winlosecounter : MonoBehaviour
{
    RuntimeData runtimeData = new RuntimeData();
    public TextMeshProUGUI winState;

    void Update()
    {

       if(Score.playerScore>Score.enemyScore){
           winState.text = ("You won!");
       }
       else if (Score.playerScore<Score.enemyScore){
           winState.text = ("You lost!");
       }
        else{
            winState.text = ("its a tie!");
        }
    }
    
}
