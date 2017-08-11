using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpawner : MonoBehaviour
{
	public GameController gameCtrl;
	void Start()
	{
		if (gameCtrl == null)
		{
			GameObject go = GameObject.Find("GameController");
			gameCtrl = (GameController)go.GetComponent(typeof(GameController));
		}
		gameCtrl.EnemySpawned(gameObject.GetInstanceID());
	}
}
