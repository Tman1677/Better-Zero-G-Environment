using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
	  // Reference to the player's heatlh.
	public GameObject enemySmall;   
	public GameObject enemyLarge;   
	// The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	Vector3 playerPos;
	public Image background;
	public Text waveText;
	int x,y,z;
	int counterSmall;
	int counterLarge;
	public float flashSpeed = 1000f;
	public int wave = 1;
	bool waveShow = true;
	public Color backColor = new Color(0,0,0,50);
	public Color textColor = new Color(255,255,255,255);
	int restartCounter = 101;
	int count = 0;

	void Start ()
	{
		
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.

	}


	void SpawnSmall ()
	{

		playerPos = GameObject.Find("Main Camera").transform.position;
		// If the player has no health left...


		// Find a random index between zero and one less than the number of spawn points.
		float distanceSpawn;
		Vector3 spawn;
		do {
			x = Random.Range (-95, 95);
			y = Random.Range (5, 195);
			z = Random.Range (-95, 95);
			spawn = new Vector3(x,y,z);
			Vector3 distance = spawn-playerPos;
			distanceSpawn = distance.magnitude;
		}while(distanceSpawn < 40);


		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemySmall, spawn, new Quaternion (0,0,0,0));
			
	}

	void Update() {
		count++;
		if (count == 100) {
			SpawnSmall ();
			count = 0;
		}
	}


}
