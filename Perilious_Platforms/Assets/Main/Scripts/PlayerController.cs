using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MattScripts {

    public class PlayerController : MonoBehaviour {

        [Header("Physics Vars")]
        [Tooltip("How much force is applied to the player while moving?")]
        [Range(1f, 500f)]
        public float moveSpeed;

        [Tooltip("How much force is applied to the player when jumping?")]
        [Range(1f, 500f)]
        public float jumpPower;

        [Tooltip("How much downward force is applied to the player while in the air?")]
        [Range(1, 10f)]
        public float gravityPower;
        [Tooltip("The max speed the player can reach when being pulled downward force?")]
        [Range(100f, 500f)]
        public float maxGravityAcceleration;

        [Header("External Refs")]
        [Tooltip("A reference to the player ridgidbody")]
        public Rigidbody rb;

        [Tooltip("References to all of the ground detection points the player used to detect for grounded")]
        public GroundDetection[] detectionPoints;

        // Private variables
        private Vector3 movement = Vector3.zero;
        private bool isGrounded = true;
        private float currGravityAcceleration = 0f;

        // Grabs player input
		private void Update()
		{
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
            movement.y = Input.GetAxis("Jump");
		}

        // Applies the player input as controls
		private void FixedUpdate()
		{
            if(movement.y > 0f && isGrounded == true)
            {
                // Jump Logic
                rb.AddForce(0f, jumpPower, 0f,  ForceMode.Impulse);
                isGrounded = false;
            }
            if(isGrounded == false)
            {
                // Gravity Logic
                rb.AddForce(0f, currGravityAcceleration,0f, ForceMode.Acceleration);
                if(Mathf.Abs(currGravityAcceleration) <= maxGravityAcceleration)
                {
                    currGravityAcceleration += -gravityPower;
                }
            }
            // General Movement
            rb.AddForce(movement.x * moveSpeed * Time.fixedDeltaTime, 0f, movement.z * moveSpeed * Time.fixedDeltaTime,  ForceMode.Impulse);
		}

        // Performs the grounded check here
		private void LateUpdate()
		{
            isGrounded = CheckIfGrounded();
		}

		// Checks if any of the grounded points are colliding with the ground
		private bool CheckIfGrounded()
        {
            foreach(GroundDetection point in detectionPoints)
            {
                if(point.IsTouchingGround == true)
                {
                    currGravityAcceleration = 0f;
                    return true;
                }
            }
            return false;
        }
	}
}