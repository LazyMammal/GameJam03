using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public GameController gameCtrl;
	public GameObject zigZag;
	public float maxLevelDuration = 10f; // time kill level
	public float minDuration = 1f;
	public bool isTransition = false, isBonus = false;
	private float levelStartTime, levelDoneTime = 0f;
	private bool levelActive = false, spawnFlag = true;
	private HashSet<int> enemySet = new HashSet<int>();

	public void Start()
	{
		SetCombatActive(false);
	}

	public void SetCombatActive(bool flag = true)
	{
		foreach (Transform child in transform)
		{
			if (child.name == "Combat")
				child.gameObject.SetActive(flag);
		}
	}
	public void DoLevelStart(int levelCode)
	{
		if (gameCtrl == null)
		{
			GameObject go = GameObject.Find("GameController");
			gameCtrl = (GameController)go.GetComponent(typeof(GameController));
		}
		levelStartTime = Time.time;
		gameCtrl.SetPlayerShip(!isTransition);
		if (!isTransition)
		{
			gameCtrl.SetTitleCard(isBonus);
			gameCtrl.SetFireActive(false);
			StartCoroutine(DoStartCombat());
		}
		else levelActive = true;
	}

	public IEnumerator DoStartCombat(float wait = 1f)
	{
		yield return new WaitForSeconds(wait);

		levelActive = true;
		gameCtrl.ClearTitleCard();
		gameCtrl.SetFireActive(true);
		SetCombatActive(true);
	}
	public void DoLevelDone()
	{
		levelActive = false;
		SetCombatActive(false);
		gameCtrl.AdvanceLevel();
	}
	void Update()
	{
		if (levelActive && (Time.time - levelStartTime > maxLevelDuration) || (levelDoneTime != 0f && Time.time > levelDoneTime))
		{
			DoLevelDone();
		}

		if (levelActive && isTransition && Input.anyKey && (Time.time - GetLevelStartTime()) > minDuration)
		{
			DoLevelDone();
		}

	}
	public void EnemySpawned(int id)
	{
		enemySet.Add(id);
		spawnFlag = true;
		//Debug.Log(enemySet.Count);
	}
	public void EnemyKilled(int id, bool playerIsKiller = true)
	{
		enemySet.Remove(id);
		//Debug.Log(enemySet.Count);
		if (spawnFlag && enemySet.Count == 0)
		{
			levelDoneTime = Time.time + 1f;
		}
	}
	public void EnemyIdle(GameObject enemy)
	{
		if (zigZag != null)
		{
			enemy.transform.parent = zigZag.transform;
			zigZag.SetActive(true);
		}
	}
	public bool isLevelActive()
	{
		return levelActive;
	}
	public float GetLevelStartTime()
	{
		return levelStartTime;
	}
}
