using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//assign this script to the players body
public class BasicMovement : MonoBehaviour {
	public GameObject player;
	[HideInInspector]
	public bool contact = true; //pretty universal test to check if touching a wall or not
	Rigidbody playerRB; //to be assigned to body of player
	Transform body;
	Transform cam; //to be assigned to the head of the player, ie camera
	public float jumpSpeed = 20; //multiplier for how fast one jumps
	RotationHandler rotationHandler;
	void Start () {
		body = transform;
		playerRB = body.GetComponent<Rigidbody>();
		cam = body.Find ("Main Camera");
		rotationHandler = body.GetComponent<RotationHandler>();
	}
	

	void Update () {
		climbing (); //code for slow movement with wasd
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (contact) {
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
		

		grab (); //code for stopping movement when wall sliding
	}



	public void sliding() {
		Vector3 tempVelocity = Quaternion.Inverse(rotationHandler.desiredRotation) * playerRB.velocity;//all of this is in an effort to 
		tempVelocity = new Vector3(tempVelocity.x,0,tempVelocity.z); //remove the "vertical" components from when you hit the wall
		playerRB.velocity = rotationHandler.desiredRotation * tempVelocity; //while retaining horizontal speed. 
		//this could almost definitely be handled better but it's what I could think up and I use this method often
	}

	void jump() {
		foreach (Transform child in transform) {
			if (child.CompareTag ("Jetpack")) {
				Jetpack jetpack = child.GetComponent<Jetpack> ();
				jetpack.fuel = 2;
			}
		}
		rotationHandler.jumpRotation (cam.forward, jumpSpeed);
		if (RotationHandler.sameObject) { //tell whether you're jumping onto the object you're already on, passed from RotationHandler
			if (Input.GetKey (KeyCode.LeftShift)) { //if so only allow iit if wall sliding
				Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * cam.forward;
				tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
				playerRB.velocity = rotationHandler.desiredRotation * tempVelocity * jumpSpeed;
			}
		} else { //otherwise jump normally
			playerRB.velocity = cam.forward * jumpSpeed;
			contact = false;
		}
	}

	void grab() { //pretty self explanatory, not the if(contact) part is in the main method
		if (contact) {
			if (Input.GetKeyUp (KeyCode.LeftShift)) {
				playerRB.velocity = new Vector3 (0, 0, 0);
			}
		}
	}

	void climbing() {
		if (contact) { /*that same nullifying vertical component method again, this time for the slow wall climbing. note, my 
			vision for this movement is to be the only type of movement uninhibited by a necessity for maintaining forward velocity,
			but as a balancing it is very slow, as if one was crawling with their hands, this is meant to encourage wall sliding
			and regular jumping and work people away from the wasd controls they are used to, but I still kept some sort of wasd
			only for small precise movements*/
			if (playerRB.velocity.magnitude < 3) {
				if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) {
					Vector3 temp = cam.forward - cam.right;
					temp = temp / temp.magnitude;
					Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * temp;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					playerRB.velocity = rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) {
					Vector3 temp = cam.forward + cam.right;
					temp = temp / temp.magnitude;
					Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * temp;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					playerRB.velocity = rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
					Vector3 temp = -cam.forward - cam.right;
					temp = temp / temp.magnitude;
					Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * temp;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					playerRB.velocity = rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
					Vector3 temp = -cam.forward + cam.right;
					temp = temp / temp.magnitude;
					Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * temp;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					playerRB.velocity = rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.W)) {
					Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * cam.forward;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					playerRB.velocity = rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.S)) {
					Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * -cam.forward;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					playerRB.velocity = rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.D)) {
					Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * cam.right;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					playerRB.velocity = rotationHandler.desiredRotation * tempVelocity;
				} else if (Input.GetKey (KeyCode.A)) {
					Vector3 tempVelocity = Quaternion.Inverse (rotationHandler.desiredRotation) * -cam.right;
					tempVelocity = new Vector3 (tempVelocity.x, 0, tempVelocity.z);
					playerRB.velocity = rotationHandler.desiredRotation * tempVelocity;
				} else {
					playerRB.velocity = new Vector3 (0, 0, 0);
				}
			}
		}
	}
}
