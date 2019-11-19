using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MattScripts {

    public class GroundDetection : MonoBehaviour {

        [Tooltip("How large is the raycast disitance to check for the ground?")]
        [Range(0.1f,0.5f)]
        public float raycastLength = 0.1f;

        [Tooltip("Who is associated with this detection point?")]
        public Rigidbody parentObject;

        // Private variables
        private bool isTouchingGround;

        // Getter
        public bool IsTouchingGround {
            get { return isTouchingGround; }
        }

        // Once this object lands on a platform object, it gets associtated with that platform.
		private void OnTriggerEnter(Collider other)
		{
            if(other.gameObject.CompareTag("Ground") && isTouchingGround == false)
            {
                if(Physics.Raycast(gameObject.transform.position, Vector3.down, raycastLength) && other.gameObject.GetComponent<Platform>() != null)
                {
                    other.gameObject.GetComponent<Platform>().objsOnPlatform.Add(parentObject);
                }
            }
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

                    if(other.gameObject.GetComponent<Platform>() != null)
                    {
                        // If we intentionally leave the platform, we are disassociated with the platform.
                        other.gameObject.GetComponent<Platform>().objsOnPlatform.Remove(parentObject);
                    }
                }
            }
        }
    }
}