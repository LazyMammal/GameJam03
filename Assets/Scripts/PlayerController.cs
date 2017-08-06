using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]

public class PlayerController : MonoBehaviour {


    public float playerShipThrust = 100.0f;
    public float playerShipAccelerationBoost = 5.0f;
    public float playerShipAccelerationMax = 10.0f;

    private float playerShipAcceleration = 1f;

    public Rigidbody2D playerShip;
    private Animator playerAnimate;


    // Use this for initialization
    void Start () {

        playerShip = GetComponent<Rigidbody2D>();
        playerAnimate = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {


        // thrust player ship left
        if (Input.GetAxis("Horizontal") < 0)
        {
            playerShip.AddForce(-transform.right * playerShipThrust * playerShipAcceleration * Time.deltaTime);


           if (playerShipAcceleration < playerShipAccelerationMax)
           {
                playerShipAcceleration += playerShipAcceleration * playerShipAccelerationBoost * Time.deltaTime;

           }

        }


        // thrust player ship right
        if (Input.GetAxis("Horizontal") > 0)
        {   
            playerShip.AddForce(transform.right * playerShipThrust * playerShipAcceleration * Time.deltaTime);

            if (playerShipAcceleration < playerShipAccelerationMax)
            {
                playerShipAcceleration += playerShipAcceleration * playerShipAccelerationBoost * Time.deltaTime;
            }

        }


        // thrust player ship up
        if (Input.GetAxis("Vertical") > 0)
        {      
            playerShip.AddForce(transform.up * playerShipThrust * playerShipAcceleration * Time.deltaTime);

            playerAnimate.SetBool("PlayerMoveUp", playerShip);

            if (playerShipAcceleration < playerShipAccelerationMax)
            {
                playerShipAcceleration += playerShipAcceleration * playerShipAccelerationBoost * Time.deltaTime;
            }

        }


        // thrust player shup down
        if (Input.GetAxis("Vertical") < 0)
        {
            playerShip.AddForce(-transform.up * playerShipThrust * playerShipAcceleration * Time.deltaTime);

            if (playerShipAcceleration < playerShipAccelerationMax)
            {
                playerShipAcceleration += playerShipAcceleration * playerShipAccelerationBoost * Time.deltaTime;
            }
        }

        else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)  
        {
            playerShipAcceleration = 1f;

            playerAnimate.SetBool("PlayerMoveUp", false);


        }




    }
}
