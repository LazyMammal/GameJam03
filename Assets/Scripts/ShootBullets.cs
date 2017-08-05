using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
	public Transform[] Bullet_Types;
	public Transform BulletSpawnPoint; // Y-forward orientation
	public float fireRate = 10f; // per second
	private float nextTime = 0.0f;

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextTime)
		{
			nextTime = Time.time + 1f / fireRate;
			ShootBullet();
		}
	}

	void ShootBullet()
	{
		// spawn bullet
		if (Bullet_Types.Length > 0)
		{
			GameObject item = Bullet_Types[Random.Range(0, Bullet_Types.Length)].gameObject;
			GameObject go = (GameObject)Instantiate(item, BulletSpawnPoint.transform.position, BulletSpawnPoint.transform.rotation);
		}
	}
}
