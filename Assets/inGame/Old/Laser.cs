using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Laser : MonoBehaviour {
	public Vector3 velocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		movement ();
	}

	void movement() {
		transform.position += (velocity * Time.deltaTime);
	}

	void OnCollisionEnter(Collision col) {
		velocity = new Vector3 (0, 0, 0);
	}
}
