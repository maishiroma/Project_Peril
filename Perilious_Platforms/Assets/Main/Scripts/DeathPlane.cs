using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MattScripts {

    public class DeathPlane : MonoBehaviour {

        // When the player enters this area, they will die immediatly.
		private void OnTriggerEnter(Collider other)
		{
            if(other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Rigidbody>().detectCollisions = false;
                other.GetComponent<PlayerController>().DeathSequence();
            }
		}
	}
}