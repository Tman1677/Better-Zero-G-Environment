using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
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
		Bullet temp = Instantiate(bulletTemplate, gunTip.transform.position, transform.rotation * Quaternion.Euler(90,0,0));
		temp.GetComponent<Rigidbody> ().velocity = player.rb.velocity + player.head.transform.forward * 30f;
		temp.shooter = player; //assign the Shooting killer to the bullet so he gets points
		#warning this isn't working for some reason 
		Destroy (temp, 2); 
	}
	#endregion

}
