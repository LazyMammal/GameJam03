using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKills : MonoBehaviour
{
	public float deathDelay = 3.0f;
	private float deathTime;

	void Start()
	{
		deathTime = Time.time + deathDelay;
	}

	void Update()
	{
		if (Time.time > deathTime)
		{
			Destroy(gameObject);
		}
	}
}
