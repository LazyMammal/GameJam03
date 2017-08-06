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
	public float speed = 7f, blendTime = 15f;
	public float spacing = 1.5f;
	private BGCurve curve1, curve2, curve3;
	private BGCcMath math1, math2, math3;
	private float startTime;

	void Start()
	{
		startTime = Time.time;

		curve1 = path1.GetComponent<BGCurve>();
		curve2 = path2.GetComponent<BGCurve>();
		math1 = path1.GetComponent<BGCcMath>();
		math2 = path2.GetComponent<BGCcMath>();

		curve3 = gameObject.AddComponent<BGCurve>();
		foreach (BGCurvePoint p in curve1.Points)
		{
			curve3.AddPoint(p);
		}
		math3 = gameObject.AddComponent<BGCcMath>();

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
		var maxDist = math3.Math.GetDistance();
		List<Transform> trList = new List<Transform>();
		List<float> ratioList = new List<float>();

		foreach (Transform t in transform)
		{
			if (t != transform)
			{
				// get current position ratio
				float curDist;
				var pos = math3.CalcPositionByClosestPoint(t.position, out curDist);
				var curRatio = curDist / maxDist;
				trList.Add(t);
				ratioList.Add(curRatio);
			}
		}

		float blendFactor = Mathf.Min(1.0f, (Time.time - startTime) / blendTime); // 0-1f

		// blend curves together
		var points1 = curve1.Points;
		var points2 = curve2.Points;
		var points3 = curve3.Points;

		for (var i = 0; i < points1.Length && i < points2.Length && i < points3.Length; i++)
		{
			points3[i].PositionLocal = Vector3.Lerp(points1[i].PositionLocal, points2[i].PositionLocal, blendFactor);
			points3[i].ControlFirstLocal = Vector3.Lerp(points1[i].ControlFirstLocal, points2[i].ControlFirstLocal, blendFactor);
			points3[i].ControlSecondLocal = Vector3.Lerp(points1[i].ControlSecondLocal, points2[i].ControlSecondLocal, blendFactor);
		}

		// recalculate math object
		math3.Recalculate();

		// useful distances
		maxDist = math3.Math.GetDistance();
		var spacingRatio = spacing / maxDist;
		float deltaRatio = Time.deltaTime * speed / maxDist;
		float distPrev = maxDist + spacing - .05f;

		// update positions in new blended curve
		float ratio = 1f + spacingRatio - 0.01f;
		for (int j = 0; j < trList.Count && j < ratioList.Count; j++)
		{
			ratio = Mathf.Min(ratioList[j] + deltaRatio, ratio - spacingRatio);
			var pos = math3.CalcByDistanceRatio(BGCurveBaseMath.Field.Position, ratio);
			trList[j].position = pos;
		}
	}

	Vector3 GetPositionDist(float distance = 0f)
	{
		var section = math3.Math[0];
		return math3.Math.CalcByDistance(BGCurveBaseMath.Field.Position, section.DistanceFromStartToOrigin + distance);
	}
}
