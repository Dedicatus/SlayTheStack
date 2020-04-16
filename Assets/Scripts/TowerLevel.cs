using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevel : MonoBehaviour
{
    enum PartType { Attack, Defense, Buff, AttackDefense, AttackBuff, DefenseBuff };
    [SerializeField] PartType myType;

    [SerializeField] private int health;
    [SerializeField] private int attack;

    [SerializeField] private int index;

    private Enemy myEnemy;
    private Tower myTower;


    private void Start()
    {
        if (transform.parent != null)
        {
            myTower = transform.parent.GetComponent<Tower>();
        }
        myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        attackEnemy();
    }

    private void Update()
    {
        if (transform.parent != null)
        {
            myTower = transform.parent.GetComponent<Tower>();
        }
    }

    private void attackEnemy()
    {
        if (myType == PartType.Attack)
        {
            myEnemy.underAttack(attack);
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void setIndex(int i)
    {
        index = i;
    }
}
