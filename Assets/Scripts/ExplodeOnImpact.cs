using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
	public GameObject explosion;
	public GameController gameCtrl;
	void Start()
	{
		if (explosion == null)
		{
			explosion = (GameObject)Resources.Load("Explosion");
		}
		if (gameCtrl == null)
		{
			GameObject go = GameObject.Find("GameController");
			gameCtrl = (GameController)go.GetComponent(typeof(GameController));
		}

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			gameCtrl.EnemyKilled(gameObject.GetInstanceID());
			GameObject go = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
