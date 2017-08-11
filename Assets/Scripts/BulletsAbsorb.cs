using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsAbsorb : MonoBehaviour {

    private Animator bossdogAnimate;
	public GameObject bulletHit;
	void Start()
	{
		if (bulletHit == null)
		{
			bulletHit = (GameObject)Resources.Load("BulletHit");
		}
    }
    void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.transform.parent.gameObject);
			Instantiate(bulletHit, other.transform.position, Quaternion.identity);
        }  
    }
}
