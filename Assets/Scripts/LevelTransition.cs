using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
	public float minDuration = 1f;
	private LevelController lvlCtrl;
	private float startTime = 0f;
	void Start()
	{
		lvlCtrl = GetComponent<LevelController>();
		startTime = Time.time;
	}
	void Update()
	{
		if (lvlCtrl.isLevelActive() && Input.anyKey && (Time.time - startTime) > minDuration)
		{
			lvlCtrl.DoLevelDone();
		}
	}
}
