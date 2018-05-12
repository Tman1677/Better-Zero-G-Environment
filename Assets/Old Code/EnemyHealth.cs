using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
	public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
	public AudioClip deathClip;                 // The sound to play when the enemy dies.
	Vector3 playerPos;
	public Rigidbody selfRB;
	public float movementSpeed;
	public GameObject self;
	GameObject player;
	public int explosionFactor;
	public int radius;
	//Animator anim;                              // Reference to the animator.
	//AudioSource enemyAudio;                     // Reference to the audio source.
	//ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
	//SphereCollider sphereCollider;            // Reference to the capsule collider.
	bool isDead;                              // Whether the enemy has started sinking through the floor.


	void Awake ()
	{
		// Setting up the references.
		//anim = GetComponent <Animator> ();
		//enemyAudio = GetComponent <AudioSource> ();
		//hitParticles = GetComponentInChildren <ParticleSystem> ();
		//capsuleCollider = GetComponent <CapsuleCollider> ();

		player = GameObject.Find("Player");
		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}


	void Update ()
	{
		// If the enemy should be sinking...

	}


	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		// If the enemy is dead...
		if(isDead)
			// ... no need to take damage so exit the function.
			return;
		
		// Play the hurt sound effect.
		//enemyAudio.Play ();

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

		// Set the position of the particle system to where the hit was sustained.
		//hitParticles.transform.position = hitPoint;

		// And play the particles.
		//hitParticles.Play();

		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			// ... the enemy is dead.
			playerPos = player.transform.position;
			Vector3 distanceT = hitPoint - playerPos;
			Death (distanceT.magnitude);
		}
	}


	void Death (float distance)
	{
		// The enemy is dead.
		isDead = true;

		playerPos = player.transform.position;
		if (self.tag == "SmallTarget") {

		}

		//if explosion happens close enough, damage the player
		if (distance < 10 * explosionFactor) {
			int rounded = (int)(distance +1 - radius);
			int rounded2 = 25 * explosionFactor/ rounded;
		}

		Destroy (self);
		// Turn the collider into a trigger so shots can pass through it.
		//sphereCollider.isTrigger = true;

		// Tell the animator that the enemy is dead.
		//anim.SetTrigger ("Dead");

		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play ();
	}


	void FixedUpdate() {
		playerPos = player.transform.position;
		Vector3 movementDirection = playerPos - transform.position;
		float speedFactor = movementSpeed / movementDirection.magnitude;


		if (movementDirection.magnitude < 4) {

			Death(movementDirection.magnitude);

		}

		movementDirection = new Vector3 (movementDirection.x * speedFactor, movementDirection.y * speedFactor, movementDirection.z * speedFactor);
		selfRB.velocity = movementDirection;

	}
}
