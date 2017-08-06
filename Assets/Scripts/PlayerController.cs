using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(BoxCollider2D))] // In sprite sub-object
[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
	public float playerShipThrust = 100.0f;
	public float playerShipAccelerationBoost = 5.0f;
	public float playerShipAccelerationMax = 10.0f;
	public float spinDuration = 1.5f, spinMultiple = 3f;

	// player explosion audio
	public AudioClip playerExplosionAudio;
	private AudioSource[] aSources;
	private AudioSource playerExplosionSource;
	private float PlayerExplosionVolLowRange = 0.5f;
	private float PlayerExplosionVolHighRange = 1.0f;

	// player engine idle audio
	public AudioClip playerEngineIdleAudio;
	private AudioSource playerEngineIdleSource;
	private float PlayerEngineIdleVolLowRange = 0.5f;
	private float PlayerEngineIdleVolHighRange = 1.0f;

	// player enging moving audio
	public AudioClip playerEngineMovingAudio;
	private AudioSource playerEngineMovingSource;
	private float PlayerEngineMovingVolLowRange = 0.5f;
	private float PlayerEngineMovingVolHighRange = 1.0f;

	// player state
	private bool playerShield, playerDead, playerSpin, playIdleSound;
	private float playerShipAcceleration = 1f;
	private float playerTargetRotation = 0f, spinStartTime = 0f, spinStartAngle = 0f;

	public Rigidbody2D playerShip;
	private Animator playerAnimate;

	// Use this for initialization
	void Start()
	{
		playerShield = true;
		playerDead = false;
		playerSpin = false;
		playIdleSound = true;

		playerShip = GetComponent<Rigidbody2D>();
		playerAnimate = GetComponentInChildren<Animator>();
		aSources = GetComponents<AudioSource>();

		playerExplosionSource = aSources[0];
		playerEngineIdleSource = aSources[1];
		playerEngineMovingSource = aSources[2];

		playerEngineIdleSource.Play();
	}

	public IEnumerator ExplosionWait()
	{
		yield return new WaitForSeconds(1.25f);

		playerAnimate.SetBool("PlayerExplosion", playerDead);       // play explosion animation
	}


	// Update is called once per frame
	void Update()
	{

		if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0)
		{
			if (playerShipAcceleration < playerShipAccelerationMax)
			{
				playerShipAcceleration += playerShipAcceleration * playerShipAccelerationBoost * Time.deltaTime;

				doThrust();
			}
		}

		// thrust player ship left
		if (Input.GetAxis("Horizontal") < 0)
		{
			playerShip.AddForce(-transform.right * playerShipThrust * playerShipAcceleration * Time.deltaTime);

		}

		// thrust player ship right
		if (Input.GetAxis("Horizontal") > 0)
		{
			playerShip.AddForce(transform.right * playerShipThrust * playerShipAcceleration * Time.deltaTime);

		}

		// thrust player ship up
		if (Input.GetAxis("Vertical") > 0)
		{
			playerShip.AddForce(transform.up * playerShipThrust * playerShipAcceleration * Time.deltaTime);

			playerAnimate.SetBool("PlayerMoveUp", playerShip);

		}

		// thrust player shup down
		if (Input.GetAxis("Vertical") < 0)
		{
			playerShip.AddForce(-transform.up * playerShipThrust * playerShipAcceleration * Time.deltaTime);

		}

		// rotate the player in a random direction by 90 degrees
		if (Input.GetButtonDown("Fire2") && !playerSpin)
		{
			// set target direction and Lerp
			playerSpin = true;
			doThrust();
			spinStartTime = Time.time;
			Transform sprite = gameObject.transform.GetChild(0).transform;
			spinStartAngle = sprite.eulerAngles.z;
			int roundAngle = Mathf.RoundToInt(spinStartAngle / 90f) * 90;
			float randAngle = Random.Range(1, 4) * 90f;
			playerTargetRotation = (roundAngle + randAngle) % 360f;
			//Debug.Log("start angle: " + spinStartAngle + ", target angle: " + playerTargetRotation + ", round angle: " + roundAngle + ", rand: " + randAngle);
		}
		else if (playerSpin)
		{
			var spinPct = (Time.time - spinStartTime) / spinDuration;
			//float angle = Mathf.LerpAngle(spinStartAngle, playerTargetRotation, spinPct );
			float angle = (spinStartAngle * (1f - spinPct) + (playerTargetRotation + 360f * spinMultiple) * spinPct);
			var sprite = gameObject.transform.GetChild(0).transform;
			var rot = sprite.eulerAngles;
			if (spinPct >= 1f)
			{
				playerSpin = false;
				doIdle();
				angle = Mathf.Round(angle / 90f) * 90f;
			}
			sprite.rotation = Quaternion.Euler(rot.x, rot.y, angle);
		}


		if (Input.GetAxis("Fire3") > 0)
		{
			playerDead = true;
			playIdleSound = false;
			playerEngineMovingSource.Stop();
			playerEngineIdleSource.Stop();
			playerExplosionSource.Play();

			StartCoroutine(ExplosionWait());  // runs a delay to launch in to the explosion animation (to sync it with the explosion audio)

			//Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 2.5f);   // destroy player object after explosion animation completes

		}

		if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
		{
			playerShipAcceleration = 1f;

			playerAnimate.SetBool("PlayerMoveUp", false);
			playerAnimate.SetBool("PlayerExplosion", false);

			doIdle();
		}
	}
	void doIdle()
	{
		// switch back to idle sound
		if (!playIdleSound && !playerDead)
		{
			playIdleSound = true;
			playerEngineIdleSource.Play();
			playerEngineMovingSource.Stop();
		}
	}

	void doThrust()
	{
		// switch to "moving" engine
		if (playIdleSound && !playerDead)
		{
			playIdleSound = false;
			playerEngineMovingSource.Play();
			playerEngineIdleSource.Stop();
		}
	}

}
