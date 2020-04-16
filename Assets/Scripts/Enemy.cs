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
	int nextAttackNumber = -1;
	int thisAttackNumber;

	[SerializeField] private int attackGap = 5;
	[SerializeField] private int attackTimer;

	private GameController myGameController;

	private SpawnController mySpawnController;

	private AttackWarningController myWarningController;

	// Start is called before the first frame update
	void Start()
	{
		myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();
		mySpawnController = GameObject.FindWithTag("System").transform.Find("SpawnController").GetComponent<SpawnController>();
		myWarningController = GameObject.FindWithTag("System").transform.Find("UIController").transform.Find("UI-World").GetChild(0).GetComponent<AttackWarningController>();
		attackMaterial = null;
		towerX = mySpawnController.getTowersX();
		attackTimer = attackGap;
		

	}

	// Update is called once per frame
	void Update()
	{
		if (attackTimer <= 0)
		{
			myGameController.gameSuspended = true;
			attack();
			attackTimer = attackGap;
		}
	}

	void attack()
	{
		if(nextAttackNumber == -1)
		{
			nextAttackNumber = Random.Range(0, 3);
		}

		thisAttackNumber = nextAttackNumber;
		//enemy cannot attack a tower twice consecutively
		Vector3 startPosition = new Vector3(towerX[thisAttackNumber], dropHeight, 0);

		if (attackMaterial == null)
		{
			attackMaterial = GameObject.Instantiate(attackMaterialPreafab, startPosition, Quaternion.identity);
		}

		while (nextAttackNumber == thisAttackNumber)
		{
			nextAttackNumber = Random.Range(0, 3);
		}
		myWarningController.isImageDisplay(false);
	}

	public void underAttack(int damage)
	{
		if (health > damage)
		{
			health -= damage;
		}

		else
		{
			defeat();
		}

	}

	public void resetAttackMaterial()
	{
		attackMaterial = null;
	}

	public void defeat()
	{

	}

	public void countTurn()
	{
		attackTimer--;
	}

	public int getHealth()
	{
		return health;
	}

	public int getTimer()
	{
		return attackTimer; 
	}

	public void addTimer(int n)
	{
		attackTimer += n;
	}
	public int getNextAttackNumber()
	{
		return nextAttackNumber;
	}

	public void gameStartWarning()
	{
		nextAttackNumber = Random.Range(0, 3);
		myWarningController.nextAttackWarning(nextAttackNumber);
	}
}
