using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//to be placed on the jetpack object
public class Jetpack : NetworkBehaviour,PlayerScript {
	#region declarations
	public int fuel = 0;
	RotationHandler rotationHandler;
	Player player;
	[HideInInspector]
	public Rigidbody rb;
	#endregion

	#region onStart
	public void onLoad() {
		attach ();
	}

	public void attach() {
		player = transform.parent.GetComponent<Player> ();
		rb = GetComponent<Rigidbody> ();
		rotationHandler = player.body.GetComponent<RotationHandler>();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		transform.localPosition = new Vector3 (0, 0, -0.6f);
		transform.localRotation = Quaternion.Euler (0, 0, -180);
	}
	#endregion

	#region onUpdate
	// Update is called once per frame
	void Update () {
		
	}
	#endregion

	#region functions
	public void boost() {
		if (fuel > 0) {
			fuel--;
			player.rb.velocity = player.rb.velocity + player.cam.transform.forward * player.boostSpeed;
			rotationHandler.jumpRotation (player.rb.velocity, player.rb.velocity.magnitude);

		}
	}
	#endregion

}
