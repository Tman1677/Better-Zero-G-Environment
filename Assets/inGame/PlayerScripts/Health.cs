using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : MonoBehaviour, PlayerScript {
	#region declarations
	public int health;
	#endregion

	#region onStart
	// Use this for initialization
	public void onLoad () {
		health = 100;
	}

	#endregion

	#region onUpdate
	// Update is called once per frame
	void Update () {
		
	}

	#endregion

	#region functions
	public void damage(Player killer) {

	}

	#endregion
}
