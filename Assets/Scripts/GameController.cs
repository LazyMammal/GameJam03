using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public GameObject[] levels;
	public GameObject startSplash, gameOver, mainGame, playerShip, scoreText, playArea, titleCard;
	public int maxPlayerLives = 3, startLevel = -1;
	public int playerScore = 0, killScore = 5;

	private int levelCode = -1;  // startSplash == -1
	private int playerLivesCount = 0, levelStageNum = 0;
	private bool isGameOver = false;
	private LevelController lvlCtrl;
	private AudioSource aSource;
	private ShootBullets[] shootBullets;

	void Awake()
	{
		startSplash.SetActive(true);
		gameOver.SetActive(false);
		mainGame.SetActive(false);
		playerShip.SetActive(false);
		playArea.SetActive(false);
		titleCard.SetActive(false);

		foreach (var lvl in levels)
		{
			if (lvl != null)
				lvl.SetActive(false);
		}
	}
	void Start()
	{
		aSource = GetComponent<AudioSource>();
		shootBullets = playerShip.GetComponentsInChildren<ShootBullets>();

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
		startSplash.SetActive(false);
		gameOver.SetActive(false);
		titleCard.SetActive(false);
		mainGame.SetActive(true);

		if (levelCode >= 0 && levelCode < levels.Length && levels[levelCode] != null)
			levels[levelCode].SetActive(false);

		if (level >= 0)
		{
			levelCode = level;
		}
		else
		{
			levelCode += 1;
		}

		while (levelCode >= 0 && levelCode < levels.Length)
		{
			if (levels[levelCode] != null)
			{
				Debug.Log("Advance Level: changing to level " + levelCode);
				levels[levelCode].SetActive(true);
				lvlCtrl = (LevelController)levels[levelCode].GetComponent(typeof(LevelController));
				lvlCtrl.DoLevelStart(levelCode);
				break;
			}
			levelCode++;
		}

		if (levelCode >= levels.Length)
		{
			DoGameOver();
		}
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
		mainGame.SetActive(false);
		playerShip.SetActive(false);
		playArea.SetActive(false);

		gameOver.SetActive(true);
		Text [] txt = gameOver.GetComponentsInChildren<Text>();
		foreach(Text t in txt)
		{
			if( t.name == "ScoreText")
				t.text = playerScore.ToString();
		}
	}

	public void SetPlayerShip(bool flag = true)
	{
		playerShip.SetActive(flag);
		playArea.SetActive(flag);
	}
	public void SetFireActive(bool flag = true)
	{
		foreach (var sb in shootBullets)
			sb.fireActive = flag;
	}

	public void SetTitleCard(bool isBonus)
	{
		titleCard.SetActive(true);
		string titleText = "STAGE 0";
		if (isBonus)
		{
			titleText = "BONUS STAGE !!";
		}
		else
		{
			titleText += ++levelStageNum;
		}
		Text txt = titleCard.GetComponentInChildren<Text>();
		txt.text = titleText;
	}

	public void ClearTitleCard()
	{
		titleCard.SetActive(false);
	}

}
