using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunRotation : NetworkBehaviour, PlayerScript {
	#region declarations

	//float rotateSpeed = 3;
	Player player;
	#endregion

	#region onStart
	// Use this for initialization
	public void onLoad () {
		player = transform.parent.transform.parent.GetComponent<Player> ();
		if (!player.isLocalPlayer) {
			transform.parent = player.armpit.transform;
			transform.localRotation = Quaternion.Euler(0,0,0);
		}
	}
	#endregion

	#region onUpdate
	// Update is called once per frame
	void Update () {
		Ray rotationAnalyze = new Ray (player.cam.transform.position, player.cam.transform.forward);
		if (!player.isLocalPlayer) {
			player.armpit.transform.rotation = Quaternion.FromToRotation (player.armpit.transform.forward, player.cam.transform.forward) * player.armpit.transform.rotation;
		}
	}
	#endregion
}
