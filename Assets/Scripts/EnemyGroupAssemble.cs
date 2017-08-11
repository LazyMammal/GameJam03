using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupAssemble : MonoBehaviour
{
	public int width = 5;
	public Vector2 spacing = new Vector2(1.5f, 1.0f);
	public float speed = 11f;
	public bool horizontal_align = true;
	public GameController gameCtrl;
	private Dictionary<int, Vector3> enemyPositions = new Dictionary<int, Vector3>();
	private int nextIndex = 0;
	private Vector3 offset;
	void Start()
	{
		if (gameCtrl == null)
		{
			GameObject go = GameObject.Find("GameController");
			gameCtrl = (GameController)go.GetComponent(typeof(GameController));
		}
		// offset for centering
		offset = Vector3.zero;
		if (horizontal_align && width > 0)
		{
			offset.x = -(width - 1) * 0.5f * spacing.x;
		}
	}
	void Update()
	{
		foreach (Transform t in transform)
		{
			if (t.CompareTag("Enemy"))
			{
				Vector3 localPos = Vector3.zero;
				if (!enemyPositions.TryGetValue(t.gameObject.GetInstanceID(), out localPos))
				{
					// needs local position assigned
					int x = nextIndex % width;
					int y = nextIndex / width;
					localPos = new Vector3(x * spacing.x, y * spacing.y, 0f) + offset;
					enemyPositions.Add(t.gameObject.GetInstanceID(), localPos);
					nextIndex++;
				}

				// adjust local position
				Vector3 curLocalPos = t.gameObject.transform.localPosition;
				float dist = Vector3.Distance(curLocalPos, localPos);
				if (dist > 0.001f)
				{
					Rigidbody2D rb = t.gameObject.GetComponent<Rigidbody2D>();
					Vector3 vec = localPos - curLocalPos;
					rb.velocity = speed * vec.normalized * Mathf.Clamp(dist, 0f, 1f);
				}
			}
		}
	}
}
