using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MattScripts {

    public class GroundDetection : MonoBehaviour {

        [Tooltip("How large is the raycast disitance to check for the ground?")]
        [Range(0.1f,0.5f)]
        public float raycastLength = 0.1f;

        // Private variables
        private bool isTouchingGround;

        // Getter
        public bool IsTouchingGround {
            get { return isTouchingGround; }
        }

        // If this point has entered ground, is it really valid?
		private void OnTriggerStay(Collider other)
		{
            if(other.gameObject.CompareTag("Ground") && isTouchingGround == false)
            {
                if(Physics.Raycast(gameObject.transform.position, Vector3.down, raycastLength))
                {
                    isTouchingGround = true;
                }
            }
		}

        // Verifies if we left the ground
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Ground") && isTouchingGround == true)
            {
                if(!Physics.Raycast(gameObject.transform.position, Vector3.down, raycastLength))
                {
                    isTouchingGround = false;
                }
            }
        }
    }
}