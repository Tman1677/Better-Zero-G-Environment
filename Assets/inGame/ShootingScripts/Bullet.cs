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
		Debug.Log (col.collider.name);
	}


	#endregion

	#region onUpdate
	void Update () {
		
	}
	#endregion
}
