using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {
	public Transform cam;
	public Transform gunTip;
	public Transform body;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray rotationAnalyze = new Ray (cam.position, cam.forward); 
		if (Physics.Raycast (rotationAnalyze, out hit)) { 
			Vector3 relativePos = hit.point - gunTip.position;
			Quaternion temp = Quaternion.LookRotation(relativePos);
			temp = Quaternion.Inverse (body.rotation) * temp;
			transform.rotation = body.rotation * Quaternion.Euler(temp.eulerAngles.x,temp.eulerAngles.y,0);
		}
	}
}
