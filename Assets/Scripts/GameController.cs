using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject[] levels;
	public GameObject startSplash, gameOver, mainGame, playerShip, scoreText;
	public int maxPlayerLives = 3, startLevel = -1;
	public int playerScore = 0, killScore = 5;

	private int levelCode = -1;  // startSplash == -1
	private int playerLivesCount = 0;
	private bool isGameOver = false;
	private LevelController lvlCtrl;
	private AudioSource aSource;

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
		aSource = GetComponent<AudioSource>();
		playerLivesCount = maxPlayerLives;
		if (startLevel >= 0 && startLevel < levels.Length)
		{
			AdvanceLevel(startLevel);
		}
	}

	// Update is called once per frame
	void Update()
	{
		// startSplash - any key
		if (!isGameOver && levelCode < 0 && Input.anyKey)
		{
			aSource.Play();
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

	public void AdvanceLevel(int level = -1)
	{
		if (level >= 0)
		{
			levelCode = level;
		}
		else
		{
			levelCode += 1;
		}

		if (levelCode >= 0 && levelCode < levels.Length)
		{
			Debug.Log("Advance Level: changing to level " + levelCode);
			startSplash.SetActive(false);
			gameOver.SetActive(false);
			mainGame.SetActive(true);
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

	public void EnemySpawned(int id)
	{
		lvlCtrl.EnemySpawned(id);
	}
	public void EnemyKilled(int id, bool playerIsKiller = true)
	{
		if (playerIsKiller)
			playerScore += killScore;
		Text txt = scoreText.GetComponent<Text>();
		txt.text = playerScore.ToString();
		lvlCtrl.EnemyKilled(id, playerIsKiller);
	}
	public void EnemyIdle(GameObject enemy)
	{
		lvlCtrl.EnemyIdle(enemy);
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
