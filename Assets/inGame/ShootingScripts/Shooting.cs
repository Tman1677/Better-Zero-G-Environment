using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
	#region declarations
	Player player;
	Transform gunTip;
	public float bulletSpeed;
	public Bullet bulletTemplate;
	int counter = 0;
	#endregion

	#region onStart
	void Start() {
		player = transform.parent.parent.GetComponent<Player>();
		gunTip = transform.Find ("GunTip");
	}
	#endregion

	#region onUpdate
	void Update () {
		if(counter>=3) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				shoot ();
				counter = 0;
			}
		}
		counter++;
	}

	#endregion

	#region functions
	void shoot() {
		Bullet temp = Instantiate(bulletTemplate, gunTip.transform.position, transform.rotation * Quaternion.Euler(90,0,0));
		temp.GetComponent<Rigidbody> ().velocity = player.rb.velocity + player.head.transform.forward * bulletSpeed;
		temp.shooter = player; //assign the Shooting killer to the bullet so he gets points
		Destroy (temp.gameObject, 20); 
	}
	#endregion

}
