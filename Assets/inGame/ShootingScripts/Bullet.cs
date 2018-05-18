using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


	#region declarations

	public Player shooter;

	#endregion

	#region onStart
	void Start () {
		
	}
	#endregion

	#region onCollision
	void OnCollisionEnter(Collision col) {
		if (col.collider.name.Contains ("smallTarget")) {
			Destroy (col.collider.gameObject);
		}
	}


	#endregion

	#region onUpdate
	void Update () {
		
	}
	#endregion
}
