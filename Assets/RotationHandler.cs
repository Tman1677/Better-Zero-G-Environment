using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//to be assigned to player body
public class RotationHandler : MonoBehaviour {
	
	RaycastHit hit; //the raycast to see which object we will be landing on, borrowed by BasicMovement via the sameObject bool
	Transform cam; //to be assigned to the head of the player, ie camera
	Transform body;
	bool rotatingCheck = false; //unecessary thing I added because I felt it would save processing power, idk if it helps
	[HideInInspector]
	public Quaternion desiredRotation;
	[HideInInspector]
	float distanceFrom; //later assigned to how far we are from where we will land
	[HideInInspector]
	float angleBetween; //later assigned as the angle between two quaternians used to decide the ideal rotation to minimize movement
	[HideInInspector]
	public static bool sameObject = false; //to be passed to BasicMovement as a second raycast seems redundant
	[HideInInspector]
	string lastObject; //used in detecting sameObject
	float jumpVelocity;

	//camera
	[HideInInspector]
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	[HideInInspector]
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float sensitivityZ = 5F;
	[HideInInspector]
	public float minimumX = -360F;
	[HideInInspector]
	public float maximumX = 360F;
	public float minimumY;
	public float maximumY;
	[HideInInspector]
	public float minimumZ = -360F;
	[HideInInspector]
	public float maximumZ = 360F;
	[HideInInspector]
	float rotationX = 0F;
	[HideInInspector]
	float rotationY = 0F;
	[HideInInspector]
	Quaternion baseBodyRotation;
	[HideInInspector]
	Quaternion baseHeadRotation;
	public float zUserRotate = 0;
	public float xUserRotate = 0;
	public float zRotate = 0;
	public float zIdealRotate = 0;
	public float xRotate = 0;
	public float yRotate = 0;
	public float xIdealRotate = 0;
	public float rotateSpeed;
	public float rotationTicker = 0;
	public Quaternion secondRotation;
	public Quaternion storedRotation;
	[HideInInspector]
	public bool rotation = true;
	public float timeStamp;



	void Start () {
		body = transform;
		cam = body.Find ("Main Camera");
		baseBodyRotation = body.localRotation;
		baseHeadRotation = cam.localRotation;
	}
	

	void Update () {
		if (rotatingCheck) {	rotating ();	} //brief check to save processing power before calling the grunt work method
		cameraRotation();


	}

	public void jumpRotation(Vector3 direction, float magnitude) { //setting up the rotation to be later done by rotating(), called by BasicMovement after jumping
		jumpVelocity = magnitude;
		Ray rotationAnalyze = new Ray (body.position, direction); 
		if (Physics.Raycast (rotationAnalyze, out hit)) { //send out raycast and get the object it hits to use, named hit
			if (hit.collider.name == lastObject) { //check if it's the same object we're already on, probably a better way to register this
				sameObject = true; //passed to BasicMovement

			} else {
				sameObject = false; //passed to BasicMovement
			}
			lastObject = hit.collider.name; //assign new name to current landing platform
			desiredRotation = hit.transform.rotation; //say this is the rotation we want

			//not ideal but best current situation I can think of to find the shortest angle
			for (int i = 0; i < 360; i++) { //this is in a goal to prevent unweildy horizontal rotating of the camera
				Quaternion temp = desiredRotation * Quaternion.AngleAxis (i, Vector3.up);
				if (Quaternion.Angle (baseBodyRotation, desiredRotation) > Quaternion.Angle (baseBodyRotation, temp)) {
					desiredRotation = temp;
				}
			}
			rotatingCheck = true;
			distanceFrom = hit.distance; //pretty much already explained this stuff above
			angleBetween = Quaternion.Angle (baseBodyRotation, desiredRotation); 
		}
	}

	void rotating() { //the slow rotations
		float step = angleBetween * Time.deltaTime * jumpVelocity / (distanceFrom); //determine optimal step value
		baseBodyRotation = Quaternion.RotateTowards(baseBodyRotation, desiredRotation, step); //rotate once a tick
		if (baseBodyRotation == desiredRotation) { //that saving processing power goal
			rotatingCheck = false;
		}
	}

	void cameraRotation() {
		//camera
		if (Time.timeScale != 0) {
			if (axes == RotationAxes.MouseXAndY) {
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;//get mouse input
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationX = ClampAngle (rotationX, minimumX, maximumX); //make it a value between -360 and 360
				rotationY = ClampAngle (rotationY, minimumY, maximumY); //make it a value between -360 and 360

				Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up); //make a Quaternion rotation out of it
				Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);//make a Quaternion rotation out of it
				body.localRotation = baseBodyRotation * xQuaternion; //apply the horizontal rotation to the body
				cam.localRotation = baseHeadRotation * yQuaternion; //apply the vertical rotation to the head
			} else if (axes == RotationAxes.MouseX)
			{
				rotationX += Input.GetAxis("Mouse X") * sensitivityX;//get mouse input
				rotationX = ClampAngle (rotationX, minimumX, maximumX); //make it a value between -360 and 360
				Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);//make a Quaternion rotation out of it
				body.localRotation = baseBodyRotation * xQuaternion; //apply the horizontal rotation to the body
			}
			else
			{
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;//get mouse input
				rotationY = ClampAngle (rotationY, minimumY, maximumY); //make it a value between -360 and 360
				Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);//make a Quaternion rotation out of it
				cam.localRotation = baseHeadRotation * yQuaternion; //apply the vertical rotation to the head
			}
		}
	}

	//camera
	public static float ClampAngle (float angle, float min, float max) { //clamp angles to between -360 and 360
		while (angle < -360F)
			angle += 360F;
		while (angle > 360F)
			angle -= 360F;
		if (angle < min)
			angle = min;
		if (angle > max)
			angle = max;
		return Mathf.Clamp (angle, min, max);
	}
}
