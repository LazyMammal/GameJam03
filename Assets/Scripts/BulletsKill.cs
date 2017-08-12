using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsKill : MonoBehaviour
{
	public GameObject explosion, bulletHit;
	public GameController gameCtrl;
	void Start()
	{
		if (explosion == null)
		{
			explosion = (GameObject)Resources.Load("Explosion");
		}
		if (bulletHit == null)
		{
			bulletHit = (GameObject)Resources.Load("BulletHit");
		}
		if (gameCtrl == null)
		{
			GameObject go = GameObject.Find("GameController");
			gameCtrl = (GameController)go.GetComponent(typeof(GameController));
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			gameCtrl.EnemyKilled(gameObject.GetInstanceID());

			Destroy(other.transform.parent.gameObject);
			Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
			Instantiate(bulletHit, other.transform.position, rot);

			rot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
			Instantiate(explosion, transform.position, rot);

			Destroy(gameObject);
		}
	}
}
