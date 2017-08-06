using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject[] levels;
	public GameObject startSplash, gameOver, mainGame, playerShip;

	private int levelCode = -1;  // startSplash == -1
	private bool isGameOver = false;

	// early start code
	void Awake()
	{
		startSplash.SetActive(true);
		gameOver.SetActive(false);
		mainGame.SetActive(false);
		playerShip.SetActive(false);

		foreach (var lvl in levels)
		{
			lvl.SetActive(false);
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// startSplash - any key
		if (!isGameOver && levelCode < 0 && Input.anyKey)
		{
			startSplash.SetActive(false);
			gameOver.SetActive(false);
			mainGame.SetActive(true);
			playerShip.SetActive(true);
			AdvanceLevel();
		}
	}

	void AdvanceLevel()
	{
		levelCode += 1;
		if( levelCode >=0 && levelCode < levels.Length )
		{
			Debug.Log("Advance Level: changing to level " + levelCode);
			levels[levelCode].SetActive(true);
		}
		else Debug.Log("Advance Level: invalid level, " + levelCode);
	}
}
