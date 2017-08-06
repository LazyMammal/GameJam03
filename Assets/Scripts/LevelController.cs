using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	private GameController gameCtrl;
	public float maxLevelDuration = 10f; // time kill level
	public bool usePlayerShip = true;
	private float levelStartTime;
	private bool levelActive = false;

	public void DoLevelStart(int levelCode)
	{
		GameObject go = GameObject.Find("GameController");
		gameCtrl = (GameController)go.GetComponent(typeof(GameController));

		levelStartTime = Time.time;
		levelActive = true;
		gameCtrl.SetPlayerShip(usePlayerShip);
	}
	public void DoLevelDone()
	{
		gameCtrl.AdvanceLevel();
		gameObject.SetActive(false);
	}
	void Update()
	{
		if (levelActive && Time.time - levelStartTime > maxLevelDuration)
		{
			levelActive = false;
			DoLevelDone();
		}
	}

	public bool isLevelActive() {
		return levelActive;
	}
}
