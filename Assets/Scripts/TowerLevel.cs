using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevel : MonoBehaviour
{
    enum PartType { Attack, Defense, Buff, AttackDefense, AttackBuff, DefenseBuff };
    [SerializeField] PartType myType;

    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private int attackBuffAmount = 20;
    [SerializeField] private int defenseBuffAmount = 20;
    [SerializeField] private int addMaterialAttackAmount = 1;
    [SerializeField] private int addMaterialShieldAmount = 2;
    [SerializeField] private int thornDamageAmount = 10;
    [SerializeField] private int permanentShirldAmount = 20;

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
        switch (myType)
        {
            case PartType.Attack:
                myTower.addAddMaterialAttackAmount(addMaterialAttackAmount);
                break;
            case PartType.Defense:
                myTower.addAddMaterialShieldAmount(addMaterialShieldAmount);
                break;
            case PartType.Buff:
                myTower.addPermanentShieldAmount(permanentShirldAmount);
                break;
            case PartType.AttackBuff:
                myTower.addDefenseBuffAmount(attackBuffAmount);
                break;
            case PartType.DefenseBuff:
                myTower.addDefenseBuffAmount(defenseBuffAmount);
                break;
            case PartType.AttackDefense:
                myTower.addThornDamageAmount(thornDamageAmount);
                break;
        }
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
