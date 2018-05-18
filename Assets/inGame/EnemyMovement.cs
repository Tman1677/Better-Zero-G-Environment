using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public GameObject player;
	Rigidbody rb;
	public float enemySpeed = 30;
	Vector3 distance;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		distance = player.transform.position - transform.position;
		rb.velocity = distance * enemySpeed / distance.magnitude;
	}
}
