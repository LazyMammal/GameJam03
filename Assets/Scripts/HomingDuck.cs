﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingDuck : MonoBehaviour
{
	private Transform playerShip;
	private Rigidbody2D rb;
	public float speed = 11f;

	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		GameObject go = GameObject.Find("PlayerShip");
		if (go != null)
		{
			playerShip = go.transform;
			Vector3 vec = playerShip.position - transform.position;
			rb.velocity = speed * vec.normalized;
		}
	}
	void Update()
	{
		if (playerShip != null)
		{
			Vector3 vec = playerShip.position - transform.position;
			rb.velocity = speed * vec.normalized;
			float flip = Vector3.Dot(vec, transform.right);
			if (flip >= 0f)
			{
				transform.localScale = new Vector3(-1f, 1f, 1f);
			}
			else
			{
				transform.localScale = Vector3.one;
			}
		}
	}
}
