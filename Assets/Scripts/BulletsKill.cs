using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsKill : MonoBehaviour
{
	public GameObject explosion;
	void Start()
	{
		if (explosion == null)
		{
			explosion = (GameObject)Resources.Load("Explosion");
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			// TODO: notify scoring system
			Destroy(other.transform.parent.gameObject);
			GameObject go = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
