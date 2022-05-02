using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon_GlowCPU : MonoBehaviour
{
    public void TurnCPU_On()
    {
        gameObject.SetActive(true);

    }
    public void TurnCPU_Off()
    {
        gameObject.SetActive(false);
    }
}
