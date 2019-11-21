using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MattScripts {

    public class UIManager : MonoBehaviour {

        [Header("External References")]
        [Tooltip("The UI that displays the current round")]
        [SerializeField]
        private TextMeshProUGUI roundText;
        [Tooltip("The UI that displays all game messages")]
        [SerializeField]
        private TextMeshProUGUI gameMessageText;

        // If not already done, clears all of the text that is associated with this script
		private void Start()
		{
            UpdateRoundText("");
            GameOverText("");
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
    }
}
