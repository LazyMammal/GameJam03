using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
	public float speed = 10f; // speed in Y-forward direction

	private float despawn_time;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0f, speed);
	}
}
