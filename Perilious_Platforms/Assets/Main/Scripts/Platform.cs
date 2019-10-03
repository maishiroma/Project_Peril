using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MattScripts {

    // The three states that all platforms will be in
    public enum PlatformState {
        isRest,
        isFalling,
        isRising
    }

    public class Platform : MonoBehaviour
    {
        [Header("Physics Variables")]
        [Tooltip("How fast does this platform move?")]
        public float moveSpeed;
        [Tooltip("What is the max speed this platform can move?")]
        public float maxAcceleration;
        [Tooltip("What is the lowest point in global space can this platform move?")]
        public float yMin;
        [Tooltip("What is the highest point in global space can this platform move?")]
        public float yMax;
        [Tooltip("How long does the platform take to rest when it hit the bottom?")]
        public float timeToRest;

        [Header("External References")]
        [Tooltip("A reference to the platform's Rigidbody component")]
        public Rigidbody rb;

        // Private Variables
        private PlatformState platformState = PlatformState.isRest;
        private float moveAcceleration = 0f;

        // Checks that all of the variables are proper values
		private void OnValidate()
		{
            moveSpeed = Mathf.Abs(moveSpeed);
            maxAcceleration = Mathf.Abs(maxAcceleration);
            timeToRest = Mathf.Abs(timeToRest);

            if(yMin >= yMax)
            {
                Debug.LogError("Y Min must be smaller than Y Max!");
            }
		}

        // Core logic of script. Checks what state the platform is in and does actions accordingly
		private void FixedUpdate()
		{
            switch(platformState)
            {
                case PlatformState.isFalling:
                    if(rb.position.y <= yMin)
                    {
                        // Makes sure the platform is stopped immediatly
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        StartCoroutine(BeginRise());
                    }
                    else
                    {
                        rb.AddForce(0f,moveAcceleration,0f, ForceMode.Acceleration);
                        if(Mathf.Abs(moveAcceleration) <= maxAcceleration)
                        {
                            moveAcceleration -= moveSpeed;
                        }
                    }
                    break;
                case PlatformState.isRising:
                    if(rb.position.y >= yMax)
                    {
                        // Makes sure the platform is stopped immediatly
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        platformState = PlatformState.isRest;
                    }
                    else
                    {
                        rb.AddForce(0f,moveAcceleration,0f, ForceMode.Acceleration);
                        if(Mathf.Abs(moveAcceleration) <= maxAcceleration)
                        {
                            moveAcceleration += moveSpeed;
                        }
                    }
                    break;
            }
		}

        // A timer that makes the platform rise once done
        private IEnumerator BeginRise()
        {
            yield return new WaitForSeconds(timeToRest);
            moveAcceleration = 0f;
            platformState = PlatformState.isRising;
        }
	
        // Called outside of the class to initiate the falling sequence
        public void StartFall()
        {
            moveAcceleration = 0f;
            platformState = PlatformState.isFalling;
        }
    }
}