using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject[] levels;
	public GameObject startSplash, gameOver, mainGame, playerShip;
	public int maxPlayerLives = 3;

	private int levelCode = -1;  // startSplash == -1
	private int playerLivesCount = 0;
	private bool isGameOver = false;
	private LevelController lvlCtrl;

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
		playerLivesCount = maxPlayerLives;
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
		else if (isGameOver)
		{
			if (Input.GetKey(KeyCode.Y))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			else if (Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.Escape) || Input.GetKey("escape"))
			{
				Debug.Log("Quit");
				Application.Quit();
			}
		}
	}

	public void AdvanceLevel()
	{
		levelCode += 1;
		if (levelCode >= 0 && levelCode < levels.Length)
		{
			Debug.Log("Advance Level: changing to level " + levelCode);
			levels[levelCode].SetActive(true);
			lvlCtrl = (LevelController)levels[levelCode].GetComponent(typeof(LevelController));
			lvlCtrl.DoLevelStart(levelCode);
		}
		else if (levelCode >= levels.Length)
		{
			DoGameOver();
		}
		else Debug.Log("Advance Level: invalid level, " + levelCode);
	}

	public void PlayerKilled()
	{
		playerLivesCount -= 1;

		if (playerLivesCount <= 0)
		{
			DoGameOver();
		}
	}

	public void DoGameOver()
	{
		isGameOver = true;
		startSplash.SetActive(false);
		gameOver.SetActive(true);
		mainGame.SetActive(false);
		playerShip.SetActive(false);
	}

	public void SetPlayerShip(bool flag = true)
	{
		playerShip.SetActive(flag);
	}
}
