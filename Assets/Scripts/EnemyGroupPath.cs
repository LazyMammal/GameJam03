using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using UnityEngine;
using BansheeGz.BGSpline.Curve;

public class EnemyGroupPath : MonoBehaviour
{
	public GameObject path1;
	public Transform[] Enemy_Types;
	public int quantity = 5;
	public float speed = 7f;
	public float spacing = 1.5f;
	private BGCurve curve1;
	private BGCcMath math1;
	private float startTime;

	void Start()
	{
		startTime = Time.time;

		curve1 = path1.GetComponent<BGCurve>();
		math1 = path1.GetComponent<BGCcMath>();

		Vector3 offset = Vector3.zero;

		// spawn enemies
		if (Enemy_Types.Length > 0)
		{
			for (int j = quantity - 1; j >= 0; j--)
			{
				Vector3 pos3 = GetPositionDist(j * spacing);
				GameObject item = Enemy_Types[Random.Range(0, Enemy_Types.Length)].gameObject;
				GameObject go = (GameObject)Instantiate(item, transform.position + pos3 + offset, transform.rotation);
				go.transform.SetParent(transform);
			}
		}

	}

	void Update()
	{
		// get children positions as ratios
		var maxDist = math1.Math.GetDistance();
		List<Transform> trList = new List<Transform>();
		List<float> ratioList = new List<float>();

		foreach (Transform t in transform)
		{
			if (t != transform)
			{
				// get current position ratio
				float curDist;
				var pos = math1.CalcPositionByClosestPoint(t.position, out curDist);
				var curRatio = curDist / maxDist;
				trList.Add(t);
				ratioList.Add(curRatio);
			}
		}

		// useful distances
		maxDist = math1.Math.GetDistance();
		var spacingRatio = spacing / maxDist;
		float deltaRatio = Time.deltaTime * speed / maxDist;
		float distPrev = maxDist + spacing - .05f;

		// update positions in curve
		float ratio = 1f + spacingRatio - 0.01f;
		for (int j = 0; j < trList.Count && j < ratioList.Count; j++)
		{
			ratio = Mathf.Min(ratioList[j] + deltaRatio, ratio - spacingRatio);
			var pos = math1.CalcByDistanceRatio(BGCurveBaseMath.Field.Position, ratio);
			trList[j].position = pos;
		}
	}

	Vector3 GetPositionDist(float distance = 0f)
	{
		var section = math1.Math[0];
		return math1.Math.CalcByDistance(BGCurveBaseMath.Field.Position, section.DistanceFromStartToOrigin + distance);
	}
}
