using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsKill : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			Destroy(other.transform.parent.gameObject);
			// TODO: spawn explosion
			// TODO: notify scoring system
			Destroy(gameObject);
		}
	}
}
