using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private int health;
	[SerializeField] private GameObject attackMaterialPreafab;
	[SerializeField] private float dropHeight;
	GameObject attackMaterial;
	float[] towerX = new float[3];

	private SpawnController mySpawnController;

	// Start is called before the first frame update
	void Start()
    {
		mySpawnController = GameObject.FindWithTag("System").transform.Find("SpawnController").GetComponent<SpawnController>();
		attackMaterial = null;
		towerX = mySpawnController.getTowersX();
	}

    // Update is called once per frame
    void Update()
    {
		attack();
    }

	void attack()
	{
		if (Input.GetKeyUp(KeyCode.Q))
		{
			Vector3 startPosition = new Vector3(towerX[Random.Range(0,3)], dropHeight, 0);

			if (attackMaterial == null)
			{
				attackMaterial = GameObject.Instantiate(attackMaterialPreafab, startPosition, Quaternion.identity);
			}
		}
	}

	public void underAttack(int damage)
	{
		if(health > damage)
		{
			health -= damage;
		}

		else
		{
			die();
		}
		
	}

	public void resetAttackMaterial()
	{
		attackMaterial = null;
	}

	public void die()
	{

	}
}
