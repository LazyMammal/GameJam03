using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using UnityEngine;
using BansheeGz.BGSpline.Curve;

public class EnemyGroupPath : MonoBehaviour
{
	public GameObject path1, path2;
	public Transform[] Enemy_Types;
	public int quantity = 5;
	public float speed = 7f;
	public float spacing = 1.5f;
	private BGCurve curve1, curve2;
	private BGCcMath math1, math2;
	private float startTime;

	void Start()
	{
		startTime = Time.time;

		curve1 = path1.GetComponent<BGCurve>();
		curve2 = path2.GetComponent<BGCurve>();
		math1 = path1.GetComponent<BGCcMath>();
		math2 = path2.GetComponent<BGCcMath>();

		Vector3 offset = Vector3.zero;

		// spawn enemies
		if (Enemy_Types.Length > 0)
		{
			for (int j = quantity-1; j >=0; j--)
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
		// useful distances
		var maxDist1 = math1.Math.GetDistance();
		var maxDist2 = math2.Math.GetDistance();

		float delta = Time.deltaTime * speed;
		float distPrev = maxDist1 + spacing - .05f;

		// find all children
		foreach (Transform t in transform)
		{
			if (t != transform)
			{
				// update positions
				float curDist;
				var pos = math1.CalcPositionByClosestPoint(t.position, out curDist);
				distPrev = Mathf.Max(curDist, Mathf.Min(curDist + delta, distPrev - spacing));
				if( distPrev > curDist + 0.01f ) {
					pos = GetPosition(distPrev);
					t.position = pos;
				}
			}
		}
	}

	Vector3 GetPosition(float distance = 0f)
	{
		var section = math1.Math[0];
		return math1.Math.CalcByDistance(BGCurveBaseMath.Field.Position, section.DistanceFromStartToOrigin + distance);
	}
}
