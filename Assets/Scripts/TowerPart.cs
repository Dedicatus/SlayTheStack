using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPart : MonoBehaviour
{
    enum PartType { Attack, Defense, Buff };
    [SerializeField] PartType myType;

    private Tower myTowerScript;

    [SerializeField] private int health;
    [SerializeField] private int attack;

    [SerializeField] private int index;
	private Tower myTowerScipt;
	private TowerShield myTowerShieldScript;
	private Enemy myEnemy;

    private void Start()
    {
		myTowerScipt = transform.parent.GetComponent<Tower>();
		myTowerShieldScript = transform.parent.Find("TowerShield").GetComponent<TowerShield>();

		myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        switch (myType)
        {
            case PartType.Attack:
                attackEnemy();
                break;
            case PartType.Defense:
				myTowerShieldScript.armorUp(myTowerScipt.getDefensePartShield());
				break;
            case PartType.Buff:
                myEnemy.addTimer(2);
                break;
        }
    }

    private void attackEnemy()
    {
        myEnemy.underAttack(attack);
    }

    public int getHealth()
	{
		return health;
	}

    public void setIndex(int i)
    {
        index = i;
    }

	public int getIndex()
	{
		return index;
	}
}
