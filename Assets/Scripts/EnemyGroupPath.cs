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
	public float speed = 7f, delay = 0f;
	public float spacing = 1.5f;
	public bool randomize = true, isHoming = true;
	private BGCcMath math1;
	private float startTime;
	private int enemyIndex = 0;
	public GameController gameCtrl;
	void Start()
	{
		if (gameCtrl == null)
		{
			GameObject go = GameObject.Find("GameController");
			gameCtrl = (GameController)go.GetComponent(typeof(GameController));
		}
		startTime = Time.time;
		math1 = path1.GetComponent<BGCcMath>();

		// spawn enemies
		if (Enemy_Types.Length > 0)
		{
			for (int j = quantity - 1; j >= 0; j--)
			{
				Vector3 pos3 = GetPositionDist(j * spacing);
				GameObject item;
				if (randomize)
				{
					item = Enemy_Types[Random.Range(0, Enemy_Types.Length)].gameObject;
				}
				else
				{
					item = Enemy_Types[(enemyIndex++) % Enemy_Types.Length].gameObject;
				}
				GameObject go = (GameObject)Instantiate(item, pos3, Quaternion.identity); // local space coordinates
				go.transform.SetParent(transform, false); // transform to world space by nesting with parent
				gameCtrl.EnemySpawned(go.GetInstanceID());
			}
		}
	}

	void Update()
	{
		if (Time.time > startTime + delay)
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
					math1.CalcPositionByClosestPoint(t.localPosition, out curDist); // compare using local position
					var curRatio = curDist / maxDist;
					trList.Add(t);
					ratioList.Add(curRatio);
				}
			}

			// useful distances
			maxDist = math1.Math.GetDistance();
			var spacingRatio = spacing / maxDist;
			float deltaRatio = Time.deltaTime * speed / maxDist;

			// update positions in curve
			float ratio = 1f + spacingRatio - 0.01f;
			for (int j = 0; j < trList.Count && j < ratioList.Count; j++)
			{
				ratio = Mathf.Min(ratioList[j] + deltaRatio, ratio - spacingRatio);
				var pos = math1.CalcByDistanceRatio(BGCurveBaseMath.Field.Position, ratio);
				trList[j].localPosition = pos; // set via local position

				// if ratio is close to 1.0, remove from list of children and zoom to player
				if (isHoming && ratio >= .95f)
				{
					trList[j].parent = null;
					trList[j].gameObject.AddComponent(typeof(HomingDuck));
					HomingDuck hd = trList[j].gameObject.GetComponent<HomingDuck>();
					hd.speed = speed * .9f;
				}
			}
		}
	}

	Vector3 GetPositionDist(float distance = 0f)
	{
		var section = math1.Math[0];
		return math1.Math.CalcByDistance(BGCurveBaseMath.Field.Position, section.DistanceFromStartToOrigin + distance);
	}
}
