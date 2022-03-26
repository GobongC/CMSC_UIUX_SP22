using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
   public Text outputText;

   private Vector2 startTouchPoisiton;
   private Vector2 currentPosition;
   private Vector2 endTouchPosition;
   private bool stopTouch = false;

   public float swipeRange;
   public float tapRange;
    // Update is called once per frame
    void Update()
    {
       Swipe(); 
    }

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPoisiton = Input.GetTouch(0).position;
            
            }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPoisiton;

            if(stopTouch)
            {
                if(Distance.y < swipeRange)
                {outputText.text = "Up";
                stopTouch = true;

            }
        }
        }
    
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPoisiton;
            if (Mathf.Abs(Distance.x)<tapRange && Mathf.Abs(Distance.y)<tapRange)
            {
                outputText.text = "tap";
            }
        }
    }
    }
}
