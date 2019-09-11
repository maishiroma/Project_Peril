using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MattScripts {

    public class PlayerController : MonoBehaviour {

        [Header("Physics Vars")]
        [Tooltip("How much force is applied to the player while moving?")]
        [Range(1f, Mathf.Infinity)]
        public float moveSpeed;

        [Tooltip("How much force is applied to the player when jumping?")]
        [Range(1f, Mathf.Infinity)]
        public float jumpPower;

        [Tooltip("How much downward force is applied to the player while in the air?")]
        [Range(-100f, -1f)]
        public float gravityPower;

        [Header("External Refs")]
        [Tooltip("A reference to the player ridgidbody")]
        public Rigidbody rb;

        // Private variables
        private Vector3 movement = Vector3.zero;
        private bool isGrounded = true;

        // Grabs player input
		private void Update()
		{
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
            movement.y = Input.GetAxis("Jump");
		}

        // Applies the player input as controls
		private void LateUpdate()
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
                rb.AddForce(0f, gravityPower,0f, ForceMode.Acceleration);
            }
            // General Movement
            rb.AddForce(movement.x * moveSpeed * Time.fixedDeltaTime, 0f, movement.z * moveSpeed * Time.fixedDeltaTime,  ForceMode.Impulse);
		}

        // TODO: Make ground detection more accurate
		private void OnCollisionStay(Collision collision)
		{
            if(collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
		}

        // TODO: Make ground detection more accurate
		private void OnCollisionExit(Collision collision)
		{
            if(collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
		}
	}
}