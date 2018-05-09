using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {


	#region declarations

	public Player shooter;

	#endregion

	#region onStart
	void Start () {
		
	}
	#endregion

	#region onCollision
	void OnCollisionEnter(Collision col) {
		Debug.Log (col.collider.name);
	}


	#endregion

	#region onUpdate
	void Update () {
		
	}
	#endregion
}
