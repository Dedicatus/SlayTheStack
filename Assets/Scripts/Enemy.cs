using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private int health;
	[SerializeField] private GameObject attackMaterialPreafab;
	[SerializeField] private float dropHeight;
	[SerializeField] private int attackDamange;
	[SerializeField] private int attackIncrement;
	GameObject attackMaterial;
	float[] towerX = new float[3];
	int nextAttackNumber = -1;
	int thisAttackNumber;

	[SerializeField] private int attackGap = 5;
	[SerializeField] private int attackTimer;

	private GameController myGameController;

	private SpawnController mySpawnController;

	private AttackWarningController myWarningController;
	private int actionTurnCount = 0;
	private int actionType = 0;
	private int turn1Action = -1;
	private int turn3Action = -1;
	private int speedUpTimes = 2;

	// Start is called before the first frame update
	void Start()
	{
		myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();
		mySpawnController = GameObject.FindWithTag("System").transform.Find("SpawnController").GetComponent<SpawnController>();
		myWarningController = GameObject.FindWithTag("System").transform.Find("UIController").transform.Find("UI-World").GetChild(0).GetComponent<AttackWarningController>();
		attackMaterial = null;
		towerX = mySpawnController.getTowersX();
		attackGap = 10;
		attackTimer = attackGap;
		

	}

	// Update is called once per frame
	void Update()
	{
		if(actionTurnCount > 3)
		{
			actionTurnCount = 1;
			
		}

		if (attackTimer <= 0)
		{
			switch (actionTurnCount)
			{
				default:
					break;
				case 0:
					attackGap = 5;
					action(0);
					break;
				case 1:
					//check last turn 3 action type
					if (turn3Action == -1)
					{
						turn1Action = Random.Range(0, 2);
					}
					else
					{
						turn1Action = 1 - turn3Action;
					}
					action(turn1Action);
					break;
				case 2:
					//check last turn 1 action type
					action(1 - turn1Action);
					break;
				case 3:
					//check current count to active speedup
					if(speedUpTimes > 0)
					{
						action(2);
						speedUpTimes--;
						turn3Action = -1;
						
					}
					else
					{
						turn3Action = Random.Range(0, 2);
						action(turn3Action);
					}
					break;

			}
			actionTurnCount++;
		}
	}

	void action(int actionType)
	{

		switch (actionType)
		{
			default:
				Debug.Log("Invalid Enemy Action");
				break;
			case 0:
				Debug.Log("Enemy Attacked " + attackDamange + " CurrentTurn " + myGameController.getCurrentTurn() + " Turn Count " + actionTurnCount);
				myGameController.gameSuspended = true;
				attack();
				break;
			case 1:
				Debug.Log("Charged" + " CurrentTurn " + myGameController.getCurrentTurn() + " Turn Count " + actionTurnCount);
				charge();
				break;
			case 2:
				Debug.Log("Speed Up" + " CurrentTurn " + myGameController.getCurrentTurn() + " Turn Count " + actionTurnCount);
				speedUp();
				break;

		}
		attackTimer = attackGap;
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

	void speedUp()
	{
		attackGap--;
		if(attackGap < 3)
		{
			attackGap = 3;
		}
	}

	void charge()
	{
		attackDamange += attackIncrement;
	}

	public void underAttack(int damage)
	{
		//if (damage > 0) { Debug.Log("DMG: " + damage); }
		
		if (health > damage)
		{
			health -= damage;
		}

		else
		{
			health = 0;
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

	public int getAttackDamage()
	{
		return attackDamange;
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
