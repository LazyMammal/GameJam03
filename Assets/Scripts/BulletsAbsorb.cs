using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsAbsorb : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			Destroy(other.transform.parent.gameObject);
			// TODO: update hitpoints
		}
	}
}
