using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
	public float nextDelta = 0.5f;
    public float rotateAngle = 90f;
	private float nextTime = 0.0f;

	void Update()
	{
		if (Input.GetButton("Jump") && Time.time > nextTime)
		{
			nextTime = Time.time + nextDelta;

            // get camera rotation
            Quaternion rot = Camera.main.transform.rotation;

            // rotate camera
            Camera.main.transform.rotation = rot * Quaternion.Euler(0, 0, rotateAngle);
		}
	}
}
