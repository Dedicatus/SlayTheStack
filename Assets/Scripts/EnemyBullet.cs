﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


	private Enemy myEnemy;
	[SerializeField] private float dropSpeed = 40.0f;
	[SerializeField] private int attack = 30;


	// Start is called before the first frame update
	void Start()
    {
        myEnemy = GameObject.FindWithTag("System").transform.Find("EnemyController").GetComponent<Enemy>();
	}

    // Update is called once per frame
    void Update()
    {
		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, -dropSpeed, 0f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "TowerMaterial")
		{
			int currentTowerHealth = collision.gameObject.GetComponent<TowerMaterial>().getHealth();
			if(attack >= currentTowerHealth)
			{
				Destroy(collision.gameObject.transform.parent.gameObject);
				attack -= currentTowerHealth;
			}
			else
			{
				Destroy(transform.parent.gameObject);
			}

			
		}

		if (collision.gameObject.tag == "Tower")
		{
			Destroy(transform.parent.gameObject);
			myEnemy.resetAttackMaterial();
		}
	}
}
