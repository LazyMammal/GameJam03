using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using UnityEngine;
using BansheeGz.BGSpline.Curve;

public class EnemyGroupPath : MonoBehaviour
{
	public GameObject path;
	public Transform[] Enemy_Types;
	public int quantity = 1;
	public float speed = 10f;
	public float spacing = 1.5f;
	private BGCurve curve;
	private BGCcMath math;
	private float startTime;

	void Start()
	{
		startTime = Time.time;

		//math = gameObject.AddComponent(typeof(BGCcMath)) as BGCcMath;
		curve = path.GetComponent<BGCurve>();
		math = path.GetComponent<BGCcMath>();

		Vector3 offset = Vector3.zero;

		// spawn enemies
		if (Enemy_Types.Length > 0)
		{
			for (int j = 0; j < quantity; j++)
			{
				Vector3 pos3 = GetPosition(j * spacing);
				GameObject item = Enemy_Types[Random.Range(0, Enemy_Types.Length)].gameObject;
				GameObject go = (GameObject)Instantiate(item, transform.position + pos3 + offset, transform.rotation);
				go.transform.SetParent(transform);
			}
		}

	}

	void Update()
	{
		// find all children
		int j = 0;
		float delta = (Time.time - startTime) * speed;
		foreach (Transform t in transform)
		{
			if (t != transform)
			{
				// update positions
				t.position = GetPosition(delta + j * spacing);
				j++;

				// TODO: remember actual positions (or else destroying ships is janky)
			}
		}
	}

	Vector3 GetPosition(float distance = 0f)
	{
		var section = math.Math[0];
		return math.Math.CalcByDistance(BGCurveBaseMath.Field.Position, section.DistanceFromStartToOrigin + distance);
	}
}
