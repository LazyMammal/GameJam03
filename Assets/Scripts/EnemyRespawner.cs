using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
	public Transform[] Enemy_Types;
	public float delay = 2f;
	public bool randomize = true;
	private float nextTime = 0f;
	private int enemyIndex = 0;

	void Update()
	{
		// spawn enemies
		if (Enemy_Types.Length > 0 && Time.time > nextTime)
		{
			nextTime = Time.time + delay;
			GameObject item;
			if (randomize)
			{
				item = Enemy_Types[Random.Range(0, Enemy_Types.Length)].gameObject;
			}
			else
			{
				item = Enemy_Types[(enemyIndex++) % Enemy_Types.Length].gameObject;
			}
			GameObject go = (GameObject)Instantiate(item, transform.position, transform.rotation);
		}
	}
}
