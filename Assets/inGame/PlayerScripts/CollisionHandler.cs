using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CollisionHandler : NetworkBehaviour, PlayerScript {
	#region declarations
	BasicMovement basicMovement;
	Player player;
	#endregion

	#region onStart
	public void onLoad() {
		player = GetComponent<Player> ();
		basicMovement = GetComponent<BasicMovement>();

	}
	#endregion
	
	#region onUpdate
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		player.contact = checkContact ();
		
	}
	#endregion

	#region onCollision
	void OnCollisionEnter(Collision col) {
		if (!isLocalPlayer) {
			return;
		}
		if (col.collider.tag == "Wall") {
			player.contact = true; //for use by other methods
			if (!Input.GetKey (KeyCode.LeftShift)) { //if not holding left shift immediately stop
				player.rb.velocity = new Vector3 (0, 0, 0);
			} else if (Input.GetKey (KeyCode.LeftShift)) { //if holding left shift allow for wall sliding
				basicMovement.sliding (); //code for wall sliding
			} //yes I realize this if and else if looks strange but I wanted to open later possibilities up
		} else if (col.collider.tag == "Jetpack") {
			equipJetpack (col.gameObject);
		} 

	}
	void OnCollisionExit(Collision col) {
		player.contact = false; //pretty basic, not quite sure if false positives from this will become a problem
		if (col.collider.tag != "Wall") {
			player.rotationHandler.jumpRotation (player.rb.velocity, player.rb.velocity.magnitude);
		}
	}
	#endregion

	#region functions
	void equipJetpack(GameObject jetpack) {
		if (!player.hasJetpack()) {
			jetpack.transform.parent = transform;
			jetpack.transform.localPosition = new Vector3 (0, 0, -0.6f);
			jetpack.transform.localRotation = Quaternion.Euler (0, 0, -180);
			Jetpack jetpackScript = jetpack.GetComponent<Jetpack>();
			jetpackScript.attach ();
		}
	}


	public  bool checkContact() {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
		for(int i = 0; i<hitColliders.Length; i++) {
			if (hitColliders[i].tag == "Wall") {
				return true;
			}
		}
		return false;
	}
	#endregion
}
