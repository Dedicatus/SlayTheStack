using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	private Enemy myEnemy;

	private Tower myTowerScript;

	private TowerScroll myTowerScrollScript;

	[SerializeField] private float dropSpeed = 40.0f;
	private int attack;

	private float destoriedHeight;
	private GameController myGameController;
	private AttackWarningController myWarningController;
	// Start is called before the first frame update
	void Start()
    {
		myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();
		myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		myWarningController = GameObject.FindWithTag("System").transform.Find("UIController").transform.Find("UI-World").GetChild(0).GetComponent<AttackWarningController>();
		attack = myEnemy.getAttackDamage();
		destoriedHeight = 0f;
	}

    // Update is called once per frame
    void Update()
    {
		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, -dropSpeed, 0f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "TowerArea")
		{
			myTowerScript = other.GetComponent<Tower>();
			myTowerScrollScript = other.GetComponent<TowerScroll>();
		}

		if (other.gameObject.tag == "TowerShield")
		{
			TowerShield myTowerShieldScript = other.gameObject.GetComponent<TowerShield>();
			int currentArmor = myTowerShieldScript.getCurrentArmor();
			if (currentArmor >= attack)
			{
				//Debug.Log("armor: " + currentArmor + " attack: " + attack);
				myTowerShieldScript.underAttack(attack);
				Destroy(gameObject);
				//myGameController.gameSuspended = false;
			}
			else
			{
				myTowerShieldScript.underAttack(currentArmor);
				attack -= currentArmor;
			}

		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "TowerMaterial")
		{
			int currentTowerHealth = collision.gameObject.GetComponent<TowerMaterial>().getHealth();
			if(attack >= currentTowerHealth)
			{
				myTowerScript.addCurHeight(-1 * (float)collision.gameObject.GetComponent<BoxCollider>().size.y);
				destoriedHeight += (float)collision.gameObject.GetComponent<BoxCollider>().size.y;
				Destroy(collision.gameObject);
				//remove tower material form tower list;
				myTowerScript.listRemoveElement();
				attack -= currentTowerHealth;
			}
			else
			{
				Destroy(gameObject);
				//myGameController.gameSuspended = false;
			}

			
		}
		


		if(collision.gameObject.tag == "TowerPart")
		{
			int currentTowerHealth = collision.gameObject.GetComponent<TowerPart>().getHealth();
			if (attack >= currentTowerHealth)
			{
				myTowerScript.addCurHeight(-1 * (float)collision.gameObject.GetComponent<BoxCollider>().size.y);
				destoriedHeight += (float)collision.gameObject.GetComponent<BoxCollider>().size.y;
				Destroy(collision.gameObject);
				//remove tower part from tower list
				for(int i = 0; i < 3; i++)
				{
					myTowerScript.listRemoveElement();
				}
				attack -= currentTowerHealth;
			}
			else
			{
				Destroy(gameObject);
				//myGameController.gameSuspended = false;
			}

		}

		if (collision.gameObject.tag == "TowerLevel")
		{
			int currentTowerHealth = collision.gameObject.GetComponent<TowerLevel>().getHealth();
			if (attack >= currentTowerHealth)
			{
				myTowerScript.addCurHeight(-1 * (float)collision.gameObject.GetComponent<BoxCollider>().size.y);
				destoriedHeight += (float)collision.gameObject.GetComponent<BoxCollider>().size.y;
				Destroy(collision.gameObject);
				//remove tower part from tower list
				for (int i = 0; i < 6; i++)
				{
					myTowerScript.listRemoveElement();
				}
				attack -= currentTowerHealth;
			}
			else
			{
				Destroy(gameObject);
				//myGameController.gameSuspended = false;
			}

		}


		if (collision.gameObject.tag == "TowerBase")
		{
			Destroy(gameObject);
			//myTowerScript.addCurHeight(-1 * (float)(collision.gameObject.transform.localScale.y / 2.0f + collision.gameObject.transform.localPosition.y));
			myTowerScript.setCurHeight(0f);
			destoriedHeight += (float)collision.gameObject.GetComponent<BoxCollider>().size.y;
			Destroy(collision.gameObject);
			myEnemy.resetAttackMaterial();
			//myGameController.gameSuspended = false;
		}
		
		if (collision.gameObject.tag == "TowerFloor")
		{
			Destroy(gameObject);
			myGameController.gameFail();
		}
		
	}

	private void OnDestroy()
	{
		if (!myGameController.isGameFailed())
		{
			if (!myGameController.isScrolling) { myGameController.gameSuspended = false; }
			myGameController.afterAttack();
			myWarningController.isImageDisplay(true);
			myWarningController.nextAttackWarning(myEnemy.getNextAttackNumber());
			if (transform.position.y < 0f)
			{
				myTowerScrollScript.scrollBack(destoriedHeight);
			}
		}
	}

	public void addAttack(int n)
	{
		attack += n;
		if (attack < 0) { attack = 0; }
	}
}
