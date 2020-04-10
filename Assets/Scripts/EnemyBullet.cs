using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	private Enemy myEnemy;

	private Tower myTowerScript;

	[SerializeField] private float dropSpeed = 40.0f;
	[SerializeField] private int attack = 30;


	// Start is called before the first frame update
	void Start()
    {
        myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
	}

    // Update is called once per frame
    void Update()
    {
		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, -dropSpeed, 0f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Tower")
		{
			myTowerScript = other.GetComponent<Tower>();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "TowerMaterial")
		{
			int currentTowerHealth = collision.gameObject.GetComponent<TowerMaterial>().getHealth();
			if(attack >= currentTowerHealth)
			{
				Destroy(collision.gameObject.transform.parent.gameObject);
				//remove tower material form tower list;
				myTowerScript.listRemoveElement();
				attack -= currentTowerHealth;
			}
			else
			{
				Destroy(transform.parent.gameObject);
			}

			
		}

		if(collision.gameObject.tag == "TowerPart")
		{
			int currentTowerHealth = collision.gameObject.GetComponent<TowerPart>().getHealth();
			if (attack >= currentTowerHealth)
			{
				Destroy(collision.gameObject.transform.parent.gameObject);
				//remove tower part from tower list
				for(int i = 0; i < 3; i++)
				{
					myTowerScript.listRemoveElement();
				}
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
