using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTheJitterBug : MonoBehaviour
{
	public Vector2 angle = new Vector2(-5f, 5f);
	public Vector2 height = new Vector2(0f, 5f);
	public Vector2 delay = new Vector2(.2f, .5f);
	private float nextTime = 0f;

	void Update()
	{
		if (Time.time > nextTime)
		{
			nextTime = Time.time + Random.Range(delay.x, delay.y);

			float rotateAngle = Random.Range(angle.x, angle.y);
			transform.rotation = Quaternion.Euler(0, 0, rotateAngle);

			Vector3 pos = transform.localPosition;
			pos.y = Random.Range(height.x, height.y);
			transform.localPosition = pos;
		}
	}
}
