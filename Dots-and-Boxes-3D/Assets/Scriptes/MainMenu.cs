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
            SceneManager.LoadScene("Game");
        }

        public void PlayGameMulti ()
        {
            SceneManager.LoadScene("multiGame");
        }

        public void ReturnToMainMenu()
        {
        SceneManager.LoadScene("Menu");
        }

        public void Restart()
        {
        SceneManager.LoadScene("Game");
        }

        public void RestartMulti()
        {
            SceneManager.LoadScene("multiGame");
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

        public void SplashScreen() {
            SceneManager.LoadScene("Menu");
        }

}
