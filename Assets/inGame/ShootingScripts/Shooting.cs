using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {
	#region declarations
	Player player;
	Transform gunTip;
	public Bullet bulletTemplate;
	#endregion

	#region onStart
	void Start() {
		player = transform.parent.parent.GetComponent<Player>();
		gunTip = transform.Find ("GunTip");
	}
	#endregion

	#region onUpdate
	void Update () {
		if (Input.GetKey (KeyCode.Mouse0)) {
			shoot ();
		}
	}

	#endregion

	#region functions
	void shoot() {
		Bullet temp = Instantiate(bulletTemplate, transform.position, transform.rotation * Quaternion.Euler(90,0,0));
		temp.GetComponent<Rigidbody> ().velocity = player.rb.velocity + player.head.transform.forward * 30f;
	}
	#endregion

}
