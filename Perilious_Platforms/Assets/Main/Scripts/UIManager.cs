using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MattScripts {

    public class UIManager : MonoBehaviour {

        [Header("External References")]
        [Tooltip("The UI that displays the current round")]
        [SerializeField]
        private TextMeshProUGUI roundText;
        [Tooltip("The UI that displays all game messages")]
        [SerializeField]
        private TextMeshProUGUI gameMessageText;
        [Tooltip("The Button used to reset the game")]
        [SerializeField]
        private Button retryButon;
        [Tooltip("The Button used to return to the title screen")]
        [SerializeField]
        private Button returnToTitleButton;
        [Tooltip("Reference to UI Image that will show the platform color")]
        [SerializeField]
        private Image flagImage;

        // If not already done, clears all of the text that is associated with this script
		private void Start()
		{
            UpdateRoundText("");
            GameOverText("");
            HideGameOverButtons();
            HideSafePlatformIndicator();
		}

        // Called in the retryButton click that resets the level
        public void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Called in the returnToTitleButton click that takes the player to the title screen
        public void ReturnToTitle()
        {
            SceneManager.LoadScene(0);
        }

        // Called externally to update the image material and show the safe plaform icon
        public void ShowSafePlatformIndicator(Material newMaterial)
        {
            flagImage.gameObject.transform.parent.gameObject.SetActive(true);
            flagImage.material = newMaterial;
        }

        // Hides the safe platform icon
        public void HideSafePlatformIndicator()
        {
            flagImage.gameObject.transform.parent.gameObject.SetActive(false);
        }

        // Called outside to easily update the text in the round text
		public void UpdateRoundText(string text)
        {
            roundText.text = text;
        }

        // Called outside to easily update the text in the round text
        public void GameOverText(string text)
        {
            gameMessageText.text = text;
        }
    
        // Called outside to show the buttons from the GameOver screen
        public void ShowGameOverButtons() 
        {
            retryButon.gameObject.SetActive(true);
            returnToTitleButton.gameObject.SetActive(true);
        }

        // Called outside to hide the buttons from the GameOver screen
        public void HideGameOverButtons() 
        {
            retryButon.gameObject.SetActive(false);
            returnToTitleButton.gameObject.SetActive(false);
        }
    }
}
