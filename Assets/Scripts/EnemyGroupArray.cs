using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupArray : MonoBehaviour
{
	public Transform[] Enemy_Types;
	public int width = 1, height = 1;
	public Vector2 spacing = new Vector2(1.5f, 1.0f);
	public bool horizontal_align = false;
	public bool vertical_align = false;

	void Start()
	{
		// offset for centering
		Vector3 offset = Vector3.zero;
		if( horizontal_align && width > 0 ) {
			offset.x = - (width-1) * 0.5f * spacing.x;
		}
		if( vertical_align && height > 0 ) {
			offset.y = - (height-1) * 0.5f * spacing.y;
		}

		// spawn enemies
		if (Enemy_Types.Length > 0)
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Vector3 pos3 = new Vector3(x * spacing.x, y * spacing.y, 0f);
					GameObject item = Enemy_Types[Random.Range(0, Enemy_Types.Length)].gameObject;
					GameObject go = (GameObject)Instantiate(item, transform.position + pos3 + offset, transform.rotation);
					go.transform.SetParent(transform);
				}
			}
		}
	}

	void Update()
	{

	}
}
