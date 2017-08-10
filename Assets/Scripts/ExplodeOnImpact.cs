using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
	public GameObject explosion;

	void Start() {
		//if( explosion == null) {
			//explosion = GameObject.Find("Explosion");
			explosion = (GameObject) Resources.Load("Explosion");
		//}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") && explosion != null)
		{
			// TODO: notify scoring system
			GameObject go = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
