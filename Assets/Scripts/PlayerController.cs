using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour {


    public float playerShipThrust = 100.0f;
    public float playerShipAccelerationBoost = 5.0f;
    public float playerShipAccelerationMax = 10.0f;



    public AudioClip playerExplosionAudio;
    private AudioSource playerExplosionSource;
    private float PlayerExplosionVolLowRange = 0.5f;
    private float PlayerExplosionVolHighRange = 1.0f;

    private bool playerShield;
    private bool playerDead;

    private float playerShipAcceleration = 1f;

    public Rigidbody2D playerShip;
    private Animator playerAnimate;



    private void Awake()
    {

        playerExplosionSource = GetComponent<AudioSource>();

    }

    // Use this for initialization
    void Start () {

        playerShield = true;
        playerDead = false;

        playerShip = GetComponent<Rigidbody2D>();
        playerAnimate = GetComponent<Animator>();

        }

    public IEnumerator ExplosionWait()
    {
        yield return new WaitForSeconds(1.25f);

        playerAnimate.SetBool("PlayerExplosion", playerDead);       // play explosion animation
    }


    // Update is called once per frame
    void Update () {

        if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0 )
        {
            if (playerShipAcceleration < playerShipAccelerationMax)
            {
                playerShipAcceleration += playerShipAcceleration * playerShipAccelerationBoost * Time.deltaTime;
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

        if (Input.GetButtonDown("Fire2")) // rotate the player in a random direction by 90 degrees
        {

            int[] directionChanger = new int[] { -1, 1 };

            gameObject.transform.Rotate(0,0, directionChanger[Random.Range(0, 2)] * 90);


        }

        if (Input.GetAxis("Fire3") > 0) {

            playerDead = true;

            playerExplosionSource.Play();

            StartCoroutine(ExplosionWait());  // runs a delay to launch in to the explosion animation (to sync it with the explosion audio)

            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 2.5f);   // destroy player object after explosion animation completes
            
        }

        


        else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            playerShipAcceleration = 1f;

            playerAnimate.SetBool("PlayerMoveUp", false);
            playerAnimate.SetBool("PlayerExplosion", false);

        }

    }

}
