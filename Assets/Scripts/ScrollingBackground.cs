using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
	public float scrollSpeed = 0.5f;
    private MeshRenderer meshRend;
	void Start()
	{
		meshRend = GetComponent<MeshRenderer>();
	}
	void Update()
	{
		float offset = Time.time * scrollSpeed;
		meshRend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
