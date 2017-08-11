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
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Kill"))
		{
			gameCtrl.EnemyKilled(gameObject.GetInstanceID(), false);
			Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
			GameObject go = (GameObject)Instantiate(explosion, transform.position, rot);
			if( other.gameObject.CompareTag("Kill") )
			{
				go.GetComponent<AudioSource>().volume = 0f;
			}
			Destroy(gameObject);
		}
	}
}
