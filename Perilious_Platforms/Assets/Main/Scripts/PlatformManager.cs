using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MattScripts {

    public class PlatformManager : MonoBehaviour {

        [Header("Game Variables")]
        [Tooltip("The round number the game is on")]
        public int roundNumber = 0;
        [Tooltip("For each X round, the speed increases of all of the platforms")]
        public int roundIteratorSpeed = 3;

        [Header("Platform Controllers")]
        [Tooltip("The amount of time it takes to have all of the platform fall down")]
        [Range(0.01f,3f)]
        public float platformPreFallTime = 3f;
        [Tooltip("The speed modifier that is added to the platorms after each round")]
        [Range(0.1f, 10f)]
        public float platformSpeedIncrementor = 1f;

        [Header("External References")]
        [Tooltip("Reference to the current player(s) in the level")]
        public PlayerController player;
        [Tooltip("Reference to the UI manager in the level")]
        public UIManager uIManager;

        //private variables
        private Platform[] arrayOfPlatforms;        // Reference to all of the platforms in the level
        private int safePlatformIndex = 0;          // The current platform that is marked safe
        private int prevIndex = 0;                  // The previous safe platform index
        private bool currentlyInRound = false;      // Is the round currently going?

        // Grabs all of the platform objects in the level automatically
		private void Start()
		{
            arrayOfPlatforms = FindObjectsOfType<Platform>();
		}

		// Keeps on raising/lowering the platforms as the game progresses
		private void Update()
		{
            if(player.isOut == false)
            {
                // This will only run when the round is considered done
                if(currentlyInRound == false)
                {
                    currentlyInRound = true;
                    roundNumber += 1;
                    uIManager.UpdateRoundText("Round " + roundNumber.ToString());

                    // We determine the safe platform
                    DetermineSafePlatform();

                    // After X seconds, all but one platform will fall
                    Invoke("StartRound", platformPreFallTime);
                }

                // While the platforms are moving, checks if the round is done. If so, we continue the loop.
                if(!IsInvoking("StartRound") && currentlyInRound == true)
                {
                    CheckForRoundOver();
                }
            }
            else
            {
                // Once the player dies, all of the platforms stop moving.
                if(!IsInvoking("StartRound") && currentlyInRound == true)
                {
                    currentlyInRound = false;

                    StopRound();
                    uIManager.GameOverText("Too Bad...");
                    uIManager.ShowGameOverButtons();
                }
            }
		}

        // Starts the round up by having all but one platform fall
        private void StartRound()
        {
            for(int currIndex = 0; currIndex < arrayOfPlatforms.Length; ++currIndex)
            {
                if(roundNumber % roundIteratorSpeed == 0)
                {
                    // Every X rounds we speed up all of the platforms
                    arrayOfPlatforms[currIndex].moveSpeed += platformSpeedIncrementor;
                    arrayOfPlatforms[currIndex].maxAcceleration += platformSpeedIncrementor;
                }
                if(currIndex != safePlatformIndex)
                {
                    // All but the safe index platform will fall
                    arrayOfPlatforms[currIndex].StartFall();
                }
            }

            // We then save the current index to check if we get duplicates
            prevIndex = safePlatformIndex;

            // And hide the safe platform indicator
            uIManager.HideSafePlatformIndicator();
        }

        // Checks if all of the platforms are reset
        private void CheckForRoundOver()
        {
            foreach(Platform currOne in arrayOfPlatforms)
            {
                if(currOne.GetPlatformState != PlatformState.isRest)
                {
                    return;
                }
            }
            currentlyInRound = false;
        }
	
        // Stops the platform logic and their movement
        private void StopRound()
        {
            foreach(Platform currOne in arrayOfPlatforms)
            {
                currOne.StopMoving();
            }
        }
    
        // We determine the safe platform and show it to the players
        private void DetermineSafePlatform()
        {
            // We make sure we do not repeat safe platforms
            while(prevIndex == safePlatformIndex)
            {
                safePlatformIndex = Random.Range(0, arrayOfPlatforms.Length);
            }

            uIManager.ShowSafePlatformIndicator(arrayOfPlatforms[safePlatformIndex].platformFlagMaterial);
        }
    }
}

