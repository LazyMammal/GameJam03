using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public GameController gameCtrl;
	public GameObject zigZag;
	public float maxLevelDuration = 10f; // time kill level
	public float minDuration = 1f;
	public bool isTransition = false;
	private float levelStartTime, levelDoneTime = 0f;
	private bool levelActive = false, spawnFlag = true;
	private HashSet<int> enemySet = new HashSet<int>();

	public void DoLevelStart(int levelCode)
	{
		if (gameCtrl == null)
		{
			GameObject go = GameObject.Find("GameController");
			gameCtrl = (GameController)go.GetComponent(typeof(GameController));
		}
		levelStartTime = Time.time;
		levelActive = true;
		gameCtrl.SetPlayerShip(!isTransition);
	}
	public void DoLevelDone()
	{
		gameCtrl.AdvanceLevel();
		gameObject.SetActive(false);
	}
	void Update()
	{
		if (levelActive && (Time.time - levelStartTime > maxLevelDuration) || (levelDoneTime != 0f && Time.time > levelDoneTime))
		{
			levelActive = false;
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
	}
	public void EnemyKilled(int id, bool playerIsKiller = true)
	{
		enemySet.Remove(id);
		if (spawnFlag && enemySet.Count == 0)
		{
			levelDoneTime = Time.time + minDuration;
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
