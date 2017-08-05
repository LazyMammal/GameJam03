using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float playerHorizontalSpeed;
    public float playerVerticalSpeed;
    

    float playerX;
    float playerY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        playerX = Input.GetAxis("Horizontal") * Time.deltaTime * playerHorizontalSpeed;
        playerY = Input.GetAxis("Vertical") * Time.deltaTime * playerVerticalSpeed;

    }
}
