using UnityEngine;
//to be assigned to player body
public class RotationHandler : MonoBehaviour, PlayerScript {
	#region declarations
	RaycastHit hit; //the raycast to see which object we will be landing on, borrowed by BasicMovement via the sameObject bool

	LayerMask wallsOnly;
	[HideInInspector]
	public Quaternion desiredRotation;
	float distanceFrom; //later assigned to how far we are from where we will land
	float angleBetween; //later assigned as the angle between two quaternians used to decide the ideal rotation to minimize movement
	[HideInInspector]
	public bool sameObject = false; //to be passed to BasicMovement as a second raycast seems redundant
	[HideInInspector]
	string lastObject; //used in detecting sameObject
	float jumpVelocity; //later used in jumpRotation()
	//int wallsLayer = -1; //possible use to make the jumping raycast layered

	//camera
	[HideInInspector]
	enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float sensitivityZ = 5F;
	float minimumX = -360F;
	float maximumX = 360F;
	public float minimumY = -85;
	public float maximumY = 85;
	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion baseBodyRotation;
	Quaternion baseHeadRotation;
	float rotateSpeed;
	Player player;
	Quaternion xQuaternion;
	Quaternion yQuaternion;
	#endregion

	#region onStart
	public void onLoad() { //called by Player script to make sure it runs in the correct order
		player = GetComponent<Player> ();
		baseBodyRotation = player.body.transform.localRotation;
		baseHeadRotation = player.head.transform.localRotation;
		wallsOnly = 1 << LayerMask.NameToLayer ("Walls");
	}
	#endregion

	#region onUpdate
	void Update () {
		rotating ();
		cameraRotation();
		slidingCurves ();

	}
	void slidingCurves () {
		if (Input.GetKey(KeyCode.LeftShift) && player.contact) { //the contact check may prove unecessary CHECK FOR REMOVAL
			if (Physics.Raycast (player.body.transform.position + player.body.transform.forward * 0.1f, /*the .1 is the increment 
			forward to check for a change in the normal position to allow smooth motion with the curve */
				    -player.body.transform.up,
				    out hit)) {
				if (hit.collider.gameObject == player.attached) {
					Quaternion change = Quaternion.FromToRotation (player.body.transform.up, hit.normal);
					baseBodyRotation = change * baseBodyRotation;
					player.rb.velocity = change * player.rb.velocity;
					//TODO needs a lot of work

				}			
			}
		} else {
			
		}
	}
	void rotating() { //the slow rotations
		if(angleBetween != 0) {
			if (Physics.Raycast (player.body.transform.position, player.rb.velocity, out hit)) {
				distanceFrom = hit.distance; //pretty much already explained this stuff above
				angleBetween = Quaternion.Angle (baseBodyRotation, desiredRotation);	
			}
			float step = angleBetween * Time.deltaTime * jumpVelocity / (distanceFrom); //determine optimal step value
			baseBodyRotation = Quaternion.RotateTowards(baseBodyRotation, desiredRotation, step); //rotate once a tick
		}
	}
	void cameraRotation() {
		//camera
		if (!player.gui.paused) {
			if (axes == RotationAxes.MouseXAndY) {
				
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;//get mouse input
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationX = ClampAngle (rotationX, minimumX, maximumX); //make it a value between minX and maxX
				rotationY = ClampAngle (rotationY, minimumY, maximumY); //make it a value between minY and maxY

				xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up); //make a Quaternion rotation out of it
				yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);//make a Quaternion rotation out of it
				player.body.transform.localRotation = baseBodyRotation * xQuaternion; //apply the horizontal rotation to the body
				player.head.transform.localRotation = baseHeadRotation * yQuaternion; //apply the vertical rotation to the head
			} else if (axes == RotationAxes.MouseX) {
				rotationX += Input.GetAxis ("Mouse X") * sensitivityX;//get mouse input
				rotationX = ClampAngle (rotationX, minimumX, maximumX); //make it a value between -360 and 360
				xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);//make a Quaternion rotation out of it
				player.body.transform.localRotation = baseBodyRotation * xQuaternion; //apply the horizontal rotation to the body
			} else {
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;//get mouse input
				rotationY = ClampAngle (rotationY, minimumY, maximumY); //make it a value between -360 and 360
				yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);//make a Quaternion rotation out of it
				player.head.transform.localRotation = baseHeadRotation * yQuaternion; //apply the vertical rotation to the head
			}
		} else { //definitely must be a better way to do this, the goal is to make the rotating still happen while paused but the camera not move with the mouse

			player.body.transform.localRotation = baseBodyRotation * xQuaternion; //apply the horizontal rotation to the body
			player.head.transform.localRotation = baseHeadRotation * yQuaternion; //apply the vertical rotation to the head
		}
	}
	#endregion

	#region functions
	public void jumpRotation(Vector3 direction, float magnitude) { //setting up the rotation to be later done by rotating(), called by BasicMovement after jumping
		jumpVelocity = magnitude;


		if (Physics.Raycast (player.body.transform.position, direction, out hit, wallsOnly)) { //send out raycast and get the object it hits to use, named hit
			//Debug.Log(hit.collider.name + " Collider up: " + hit.transform.up + " Player up: " + player.body.transform.up + " " + desiredRotation);
		
			//if (hit.collider.tag == "Wall") {
				//this turns desiredRotation into one that has the right vertical alignment using the FromToRotation but keeps the original other rotations

			desiredRotation = Quaternion.FromToRotation (player.body.transform.up, hit.normal) * baseBodyRotation; //order matters for global rotation

			angleBetween = -1; //start rotation to rotating()
			//}
			if (hit.collider.name == lastObject) { //check if it's the same object we're already on, probably a better way to register this
				sameObject = true; //passed to BasicMovement

			} else {
				sameObject = false; //passed to BasicMovement
			}
			lastObject = hit.collider.name; //assign new name to current landing platform
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
	#endregion

}
