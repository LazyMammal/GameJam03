using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagDescend : MonoBehaviour
{
	// setup
	public float speed = 1f; // speed in X-axis
	public float verticalSpacing = 1.5f; // step size
	public Vector2 horizontalLimits = new Vector2(-5f, 5f); // min,max
	public Vector2 verticalLimits = new Vector2(-10f, 10f); // min,max

	// state
	bool descending = false;
	private Vector2 velocity;
	private Vector3 waypoint;

	void Start()
	{
		descending = false;
		velocity = new Vector2(-speed, -speed);
		waypoint = transform.position;
	}

	void Update()
	{
		var pos = transform.position;

		// check if descent is complete
		if (descending && Mathf.Abs(waypoint.y - pos.y) >= verticalSpacing)
		{
			descending = false;
			velocity.x *= -1f;
			waypoint = pos;
		}

		// check if zig/zag is complete
		else if (!descending && (pos.x < horizontalLimits.x || pos.x > horizontalLimits.y))
		{
			descending = true;
			waypoint = pos;
		}

		// move group along only one axis
		Vector3 newPos = pos;
		pos.x = Mathf.Clamp(pos.x, horizontalLimits.x, horizontalLimits.y);
		pos.y = Mathf.Clamp(pos.y, verticalLimits.x, verticalLimits.y);

		if (descending)
		{
			newPos = new Vector3(pos.x, pos.y + velocity.y * Time.deltaTime, pos.z);
		}
		else
		{
			newPos = new Vector3(pos.x + velocity.x * Time.deltaTime, pos.y, pos.z);
		}

		transform.position = newPos;
	}
}
