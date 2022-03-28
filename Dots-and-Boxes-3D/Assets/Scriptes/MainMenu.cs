using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Main main;
        public void PlayGame ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        }

        public void changeGrid6 () { 
            Main.Width = 6;
            Main.Height = 6;
        }

        public void changeGrid7 () { 
            Main.Width = 7;
            Main.Height = 7;
        }

}
