using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPart : MonoBehaviour
{
    enum PartType { Attack, Defense, Buff };
    [SerializeField] PartType myType;

    private Tower myTowerScript;
    private TowerShield myTowerShieldScript;
    private Enemy myEnemy;
    private bool triggered = false;

    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private int armor;

    [SerializeField] private int index;

	

    private void Start()
    {
        towerPartHandler();
    }

    private void towerPartHandler()
    {
        myTowerScript = transform.parent.GetComponent<Tower>();
        myTowerShieldScript = transform.parent.Find("TowerShield").GetComponent<TowerShield>();

        myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

        if (!triggered)
        {
            switch (myType)
            {
                case PartType.Attack:
                    attackEnemy();
                    break;
                case PartType.Defense:
                    myTowerShieldScript.armorUp(armor + myTowerScript.getDefenseBuffAmount());
                    break;
                case PartType.Buff:
                    myEnemy.addTimer(2);
                    break;
            }
            triggered = true;
        }
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        towerPartHandler();
    }

    private void attackEnemy()
    {
        myEnemy.underAttack(attack + myTowerScript.getAttackBuffAmount());
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
