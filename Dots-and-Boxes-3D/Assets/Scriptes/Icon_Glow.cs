using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon_Glow : MonoBehaviour
{
    public void TurnP1_On()
    {
        gameObject.SetActive(true);
       
    }
    public void TurnP1_Off()
    {
        gameObject.SetActive(false);
    }
}
