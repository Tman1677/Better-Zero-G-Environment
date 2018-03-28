using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//assign this script to the players body
public class BasicMovement : NetworkBehaviour, PlayerScript {
	#region declarations
	Player player;
	#endregion

	#region onStart
	public void onLoad() {
		player = GetComponent<Player> ();


	}
	#endregion
	
	#region onUpdate
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		climbing (); //code for slow movement with wasd
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (player.contact) {
				jump (); //code for jumping
			} else {
				foreach (Transform child in transform) {
					if (child.CompareTag ("Jetpack")) {
						Jetpack jetpack = child.GetComponent<Jetpack> ();
						jetpack.boost ();
					}
				}
			}
		}
	}
	void LateUpdate() {
		if (!isLocalPlayer) {
			return;
		}
		grab (); //code for stopping movement when wall sliding
	}
	void grab () { //pretty self explanatory, not the if(contact) part is in the main method
		if (Input.GetKeyUp (KeyCode.LeftShift)) {//yes I reordered this because I'm a shitty programmer and it stopped an error
			if (player.contact) {
				player.rb.velocity = new Vector3 (0, 0, 0);
			}
		}
	}
	void jump() {
		foreach (Transform child in transform) {
			if (child.CompareTag ("Jetpack")) {
				Jetpack jetpack = child.GetComponent<Jetpack> ();
				jetpack.fuel = 2;
			}
		}
		player.rotationHandler.jumpRotation (player.cam.transform.forward, player.jumpSpeed);
		if (player.rotationHandler.sameObject) { //tell whether you're jumping onto the object you're already on, passed from RotationHandler
			if (Input.GetKey (KeyCode.LeftShift)) { //if so only allow iit if wall sliding
				Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * player.cam.transform.forward;
				tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
				player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity * player.jumpSpeed;
			}
		} else { //otherwise jump normally
			player.rb.velocity = player.cam.transform.forward * player.jumpSpeed;
			player.contact = false;
		}
	}
	void climbing() {
		if (player.contact) { /*that same nullifying vertical component method again, this time for the slow wall climbing. note, my 
			vision for this movement is to be the only type of movement uninhibited by a necessity for maintaining forward velocity,
			but as a balancing it is very slow, as if one was crawling with their hands, this is meant to encourage wall sliding
			and regular jumping and work people away from the wasd controls they are used to, but I still kept some sort of wasd
			only for small precise movements*/
			if (player.rb.velocity.magnitude < 3) {
				if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) {
					Vector3 temp = player.cam.transform.forward - player.cam.transform.right;
					temp = temp / temp.magnitude;
					Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * temp;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) {
					Vector3 temp = player.cam.transform.forward + player.cam.transform.right;
					temp = temp / temp.magnitude;
					Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * temp;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
					Vector3 temp = -player.cam.transform.forward - player.cam.transform.right;
					temp = temp / temp.magnitude;
					Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * temp;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
					Vector3 temp = -player.cam.transform.forward + player.cam.transform.right;
					temp = temp / temp.magnitude;
					Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * temp;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.W)) {
					Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * player.cam.transform.forward;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.S)) {
					Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * -player.cam.transform.forward;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.D)) {
					Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * player.cam.transform.right;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.A)) {
					Vector3 tempVelocity = Quaternion.Inverse (player.rotationHandler.desiredRotation) * -player.cam.transform.right;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity;
				} else {
					player.rb.velocity = new Vector3 (0, 0, 0);
				}
			}
		}
	}
	#endregion

	#region functions
	public void sliding() {
		Vector3 tempVelocity = Quaternion.Inverse(player.rotationHandler.desiredRotation) * player.rb.velocity;//all of this is in an effort to 
		tempVelocity = new Vector3(tempVelocity.x,0,tempVelocity.z); //remove the "vertical" components from when you hit the wall
		player.rb.velocity = player.rotationHandler.desiredRotation * tempVelocity; //while retaining horizontal speed. 
		//this could almost definitely be handled better but it's what I could think up and I use this method often
	}
	#endregion

}
