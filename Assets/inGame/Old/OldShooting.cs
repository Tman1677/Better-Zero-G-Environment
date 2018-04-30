using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class OldShooting : MonoBehaviour {
	public Laser laser;
	Transform cam;
	Transform gunTip;
	//Transform body;
	RaycastHit hit;
	public float shotSpeed = 30;
	// Use this for initialization
	void Start () {
		cam = transform.parent;
		//body = cam.parent;
		gunTip = transform.Find("GunTip");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			
			Laser newLaser = Instantiate<Laser> (laser, gunTip.position + transform.forward * .5f,transform.rotation * Quaternion.Euler(90,0,0)); //just in there for base rotation of laser, definitely a better way to do this
			if (Physics.Raycast (cam.position, cam.forward, out hit)) { //send out raycast and get the object it hits to use, named hit
				if (hit.distance >= 2) {
					Vector3 direction = hit.point - gunTip.position;
					direction = direction / direction.magnitude;
					Laser script = newLaser.GetComponent<Laser> ();
					script.velocity = direction * shotSpeed;
					Debug.Log (script.velocity);
				} else {
					Laser script = newLaser.GetComponent<Laser> ();
					script.velocity = transform.forward * shotSpeed;
					Debug.Log (script.velocity);
				}
			} else {
				Laser script = newLaser.GetComponent<Laser> ();
				script.velocity = transform.forward * shotSpeed;
				Debug.Log (script.velocity);
			}

		}
	}
}
