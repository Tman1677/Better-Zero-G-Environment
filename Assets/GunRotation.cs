using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {
	Transform cam;
	//Transform gunTip;
	Transform body;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		cam = transform.parent;
		body = cam.parent;
		//gunTip = transform.Find("GunTip");
	}
	
	// Update is called once per frame
	void Update () {
		Ray rotationAnalyze = new Ray (cam.position, cam.forward); 
		if (Physics.Raycast (rotationAnalyze, out hit)) { 
			Vector3 relativePos = hit.point - transform.position;
			Quaternion temp = Quaternion.LookRotation(relativePos);
			temp = Quaternion.Inverse (body.rotation) * temp;
			Quaternion desiredRotation = body.rotation * Quaternion.Euler(temp.eulerAngles.x,temp.eulerAngles.y,0);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRotation, 1f);
		}
	}
}
