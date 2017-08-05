using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float playerShipThrust;
    public float playerShipAccelerationBoost;
    public float playerShipAccelerationMax;

    private float playerShipAcceleration = 1f;
    public Rigidbody2D playerShip;

    // Use this for initialization
    void Start () {

        playerShip = GetComponent<Rigidbody2D>();

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


           
           //transform.Rotate(0, Time.deltaTime, 0, 0);

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

        }




    }
}
