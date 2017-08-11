using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetAndReturn : MonoBehaviour
{
	public float duration = 1.5f;
	public Vector3 minOffset = new Vector3(0f, 1f, 0f);
	public Vector3 maxOffset = new Vector3(0f, 2f, 0f);
	private Vector3 savedLocalPosition, savedRandPosition;
	private float startTime;

	void Start()
	{
		savedLocalPosition = transform.localPosition;
		Vector3 offsetPos = new Vector3(
			Random.Range(minOffset.x, maxOffset.x),
			Random.Range(minOffset.y, maxOffset.y),
			Random.Range(minOffset.z, maxOffset.z)
		);

		transform.localPosition += offsetPos;
		savedRandPosition = transform.localPosition;

		startTime = Time.time;
	}
	void Update()
	{
		transform.localPosition = Vector3.Lerp(savedRandPosition, savedLocalPosition, (Time.time - startTime) / duration);
	}
}
