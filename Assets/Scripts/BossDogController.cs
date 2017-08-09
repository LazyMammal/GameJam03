using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDogController : MonoBehaviour {

    private Animator bossdogAnimate;
    private GameObject bossDogGameObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {

            GetComponent<Animator>().SetBool("BossDogHit", true);

            Invoke("SetBoolBack", 2);
            

        }
    }
    private void SetBoolBack()
    {
        GetComponent<Animator>().SetBool("BossDogHit", false);
    }
}