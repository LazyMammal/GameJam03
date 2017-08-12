using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDogController : MonoBehaviour
{
	public int bossDogHealth = 255, numBossExplosions = 3;
	public GameObject explosion;
	private Animator bossdogAnimate;
	private AudioSource aSource;
	public GameController gameCtrl;
	private bool isFirstDead = true;
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

			if (bossDogHealth <= 0 && isFirstDead)
			{
				isFirstDead = false;
				GetComponent<Animator>().SetBool("BossDogHit", true);
				aSource.Play();
				foreach (Transform t in transform)
				{
					if (t.gameObject.GetComponent(typeof(EnemyRespawner)))
					{
						t.gameObject.SetActive(false);
					}
				}
				gameCtrl.SetFireActive(false);
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
		StartCoroutine(ExplodeWait(0f));
		for (int i = 1; i < numBossExplosions; i++)
		{
			StartCoroutine(ExplodeWait(Random.Range(0f, .5f)));
		}

		yield return new WaitForSeconds(.5f);
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		sr.enabled = false;

		yield return new WaitForSeconds(3f);
		gameCtrl.EnemyKilled(gameObject.GetInstanceID());
	}

	public IEnumerator ExplodeWait(float wait = 0f)
	{
		yield return new WaitForSeconds(wait);

		Vector3 pos = transform.position + new Vector3(
			Random.Range(-1f, 1f),
			Random.Range(-1f, 1f),
			0f
		);
		Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
		GameObject go = (GameObject)Instantiate(explosion, pos, rot);
		go.transform.localScale *= 3f;
	}

}