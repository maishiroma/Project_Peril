using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MattScripts {

    public class MenuManager : MonoBehaviour {

        // Called from a button UI. Loads the main game up.
        public void GoToGame()
        {
            SceneManager.LoadScene(1);
        }

        // Called from a button UI. Closes the game
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}