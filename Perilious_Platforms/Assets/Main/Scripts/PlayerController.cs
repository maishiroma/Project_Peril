using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MattScripts {

    public class PlayerController : MonoBehaviour {

        [Header("General Vars")]
        [Tooltip("Is the player still in the game?")]
        public bool isOut = false;

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

        [Tooltip("How fast does the player move when they die?")]
        [Range(100f, 500f)]
        public float deathMoveSpeed;

        [Header("External Refs")]
        [Tooltip("A reference to the player ridgidbody")]
        public Rigidbody rb;

        [Tooltip("References to all of the ground detection points the player used to detect for grounded")]
        public GroundDetection[] detectionPoints;

        // Private variables
        private Vector3 movement = Vector3.zero;
        private bool isGrounded = true;
        private float currGravityAcceleration = 0f;
        private Vector3 deathMovementDir = Vector3.zero;

        // Grabs player input
		private void Update()
		{
            if(isOut == false)
            {
                movement.x = Input.GetAxis("Horizontal");
                movement.z = Input.GetAxis("Vertical");
                movement.y = Input.GetAxis("Jump");
            }
            else
            {
                // If the player is out of the game, they cannot move
                movement = Vector3.zero;
            }
		}

        // Applies the player input as controls
		private void FixedUpdate()
		{
            if(isOut == false)
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
            else
            {
                rb.AddForce(deathMovementDir * moveSpeed * Time.fixedDeltaTime);
            }
		}

        // Performs the grounded check here
		private void LateUpdate()
		{
            if(isOut == false)
            {
                isGrounded = CheckIfGrounded();
            }
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
	
        // Gets called when the player is out of the game.
        public void DeathSequence()
        {
            if(isOut == false)
            {
                deathMovementDir = new Vector3(deathMoveSpeed,deathMoveSpeed,deathMoveSpeed);
                for(int count = 0; count < 2; count++)
                {
                    int randomDir = Random.Range(0,2);
                    int randomSpeed = Random.Range(0,2);
                    switch(count)
                    {
                        case 0:
                            if(randomSpeed == 1)
                            {
                                deathMovementDir.x *= -randomDir;
                            }
                            else
                            {
                                deathMovementDir.x *= randomDir;
                            }
                            break;
                        case 1:
                            if(randomSpeed == 1)
                            {
                                deathMovementDir.z *= -randomDir;
                            }
                            else
                            {
                                deathMovementDir.z *= randomDir;
                            }
                            break;
                    }
                }
                isOut = true;
            }
        }
    }
}