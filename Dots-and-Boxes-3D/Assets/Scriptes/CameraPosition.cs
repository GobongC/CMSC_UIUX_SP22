using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
   
    public void move(Vector3 position)
    {
        this.transform.position = position;
    }
}

