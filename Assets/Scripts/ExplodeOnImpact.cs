using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
	public Animator anim;

	void Start() {
		anim = GetComponentInChildren<Animator>();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			anim.SetBool("Explode", true);
			// TODO: notify scoring system
			//Destroy(gameObject);
		}
	}
}
