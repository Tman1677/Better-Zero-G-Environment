using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour {
	#region declarations
	[HideInInspector]
	public bool contact = true; //pretty universal test to check if touching a wall or not
	public float boostSpeed = 20;
	[HideInInspector]
	public Rigidbody rb;
	[HideInInspector]
	public GameObject cam;
	[HideInInspector]
	public GameObject head;
	[HideInInspector]
	public GameObject body;
	[HideInInspector]
	public GameObject mainCamera;
	[HideInInspector]
	public BasicMovement basicMovement;
	[HideInInspector]
	public RotationHandler rotationHandler;
	[HideInInspector]
	public CollisionHandler collisionHandler;
	[HideInInspector]
	public GUICode gui;
	[HideInInspector]
	public GameObject armpit;
	[HideInInspector]
	public GameObject gun;

	[HideInInspector]
	public GunRotation gunRotation;
	Jetpack jetpack;
	public float jumpSpeed = 20; //multiplier for how fast one jumps
	List<PlayerScript> playerScripts = new List<PlayerScript>();
	#endregion

	#region onStart


	void Start() {

		mainCamera = GameObject.Find ("Main Camera");
		cam = transform.Find ("Head").Find ("Camera").gameObject;
		head = transform.Find ("Head").gameObject;
		gun = getGun ();
		rb = GetComponent<Rigidbody> ();
		body = this.gameObject;
		armpit = transform.Find ("Armpit").gameObject;
		basicMovement = GetComponent<BasicMovement> ();
		collisionHandler = GetComponent<CollisionHandler> ();
		rotationHandler = GetComponent<RotationHandler> ();
		gunRotation = gun.GetComponent<GunRotation> ();
		gui = GetComponent<GUICode> ();

		playerScripts.Add (basicMovement);
		playerScripts.Add (collisionHandler);
		playerScripts.Add (rotationHandler);
		playerScripts.Add (gui);
		playerScripts.Add (gunRotation);

		if(hasJetpack()) { //Run the load function on the jetpack if it exists
			jetpack = getJetpack ().GetComponent<Jetpack> ();
			playerScripts.Add (jetpack);
		}
		foreach(PlayerScript script in playerScripts) {
			script.onLoad ();
		}

		localStart ();
		
	}

	void localStart() { //initialization of player and calling player scripts loads in the correct order
		if (!isLocalPlayer) {
			return;
		}

		mainCamera.GetComponent<Camera>().enabled = false;
		cam.GetComponent<Camera>().enabled = true;
		mainCamera.GetComponent<AudioListener> ().enabled = false;
		cam.GetComponent<AudioListener> ().enabled = true;
		transform.position = new Vector3 (0, -48.5f, 0);
		rb.velocity = new Vector3 (0, 0, 0);
		contact = true;

	}
	#endregion

	#region helpterFunctions
	public GameObject getJetpack() {
		foreach(Transform child in transform) {
			if (child.CompareTag("Jetpack")) {
				return child.gameObject;
			}
		}
		return null;
	}
	public GameObject getGun() {
		Debug.Log ("test");
		foreach(Transform child in head.transform) {
			Debug.Log (child.name);
			if (child.CompareTag("Gun")) {
				return child.gameObject;
			}
		}
		return null;
	}
	public bool hasJetpack() {
		foreach(Transform child in transform) {
			if (child.CompareTag("Jetpack")) {
				return true;
			}
		}
		return false;
	}
	#endregion

	#region onUpdate
	// Update is called once per frame
	void Update () {
		
	}
	#endregion

}
