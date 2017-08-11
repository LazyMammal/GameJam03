using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDogController : MonoBehaviour
{
	public int bossDogHealth = 255;
	private Animator bossdogAnimate;
	private AudioSource aSource;
	public GameController gameCtrl;
	void Start()
	{
		if (gameCtrl == null)
		{
			GameObject go = GameObject.Find("GameController");
			gameCtrl = (GameController)go.GetComponent(typeof(GameController));
		}
		aSource = GetComponent<AudioSource>();
	}
	void Update()
	{
		GetComponent<SpriteRenderer>().color = new Color32(255, (byte)bossDogHealth, (byte)bossDogHealth, 255);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bullet")
		{
			if (bossDogHealth > 0)
			{
				GetComponent<Animator>().SetBool("BossDogHit", true);
				Invoke("SetBoolBack", 0.5f);
				bossDogHealth--;
			}

			if (bossDogHealth <= 0)
			{
				GetComponent<Animator>().SetBool("BossDogHit", true);
				aSource.Play();
				foreach (Transform t in transform)
				{
					if (t.gameObject.GetComponent(typeof(EnemyRespawner)))
					{
						t.gameObject.SetActive(false);
					}
				}
				StartCoroutine(DeathWait());
			}
		}
	}
	private void SetBoolBack()
	{
		GetComponent<Animator>().SetBool("BossDogHit", false);
	}

	public IEnumerator DeathWait()
	{
		yield return new WaitForSeconds(2f);
		gameCtrl.EnemyKilled(gameObject.GetInstanceID());
		Destroy(gameObject);
	}

}