using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    Vector3 myVector;


    public Main main;
    
        public void PlayGame ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void PlayGameMulti ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        public void ReturnToMainMenu()
        {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public void Restart()
        {
        SceneManager.LoadScene("Game");
        }

        public void changeGrid5 () { 
            Main.Width = 5;
            Main.Height = 5;
            MultiMain.MultiWidth = 5;
            MultiMain.MultiHeight = 5;
            myVector = new Vector3(3f,4f,2.25f);
            //CameraPosition.move(myVector);
        }

        public void changeGrid6 () { 
            Main.Width = 6;
            Main.Height = 6;
            MultiMain.MultiWidth = 6;
            MultiMain.MultiHeight = 6;
            myVector = new Vector3(3f,4f,2.25f);
            //CameraPosition.move(myVector);
        }

        public void changeGrid7 () { 
            Main.Width = 7;
            Main.Height = 7;
            MultiMain.MultiWidth = 7;
            MultiMain.MultiHeight = 7;
            myVector = new Vector3(3f,4f,2.25f);
            //CameraPosition.move(myVector);
        }

}
