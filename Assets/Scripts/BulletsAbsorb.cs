using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsAbsorb : MonoBehaviour {

    private Animator bossdogAnimate;

    void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.transform.parent.gameObject);
            // TODO: update hitpoints


        }  
    }
}
