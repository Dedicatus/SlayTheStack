using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFieldUIController : MonoBehaviour
{

	[SerializeField] private Image cdShadow;
	[SerializeField] private Image attackIcon;
	[SerializeField] private Image powerUpIcon;
	[SerializeField] private Image speedUpIcon;
	[SerializeField] private Image attackState;
	[SerializeField] private Image powerUpState;
	[SerializeField] private Image speedUpState;
	[SerializeField] private Text attackNumber;
	[SerializeField] private Image bossUnderAttack;
	[SerializeField] private Text bossUnderAttackText;
	private Enemy myEnemy;

	private int attackGap;
	private int attackTimer;
	private float damageTimer = 1.0f;
	// Start is called before the first frame update
	void Start()
    {
		myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
	}

    // Update is called once per frame
    void Update()
    {
		attackGap = myEnemy.getAttackGap();
		attackTimer = myEnemy.getTimer();

		cdShadow.fillAmount = (float)attackTimer / attackGap;
		attackNumber.text = "" + myEnemy.getAttackDamage();

		if(bossUnderAttack.enabled == true && bossUnderAttackText.enabled == true)
		{
			damageTimer -= Time.deltaTime;
		}
		if(damageTimer <= 0.0f)
		{
			bossUnderAttack.enabled = false;
			bossUnderAttackText.enabled = false;
			damageTimer = 1.0f;
		}

	}

	public void actionState(int state)
	{
		switch (state)
		{
			default:
				break;
			case 0:
				attacking();
				break;
			case 1:
				powerUping();
				break;
			case 2:
				speedUping();
				break;
		}
	}

	public void attacking()
	{
		attackIcon.enabled = true;
		powerUpIcon.enabled = false;
		speedUpIcon.enabled = false;
		attackState.enabled = true;
		powerUpState.enabled = false;
		speedUpState.enabled = false;
		attackNumber.enabled = true;
	}

	public void powerUping()
	{
		attackIcon.enabled = false;
		powerUpIcon.enabled = true;
		speedUpIcon.enabled = false;
		attackState.enabled = false;
		powerUpState.enabled = true;
		speedUpState.enabled = false;
		attackNumber.enabled = false;
	}

	public void speedUping()
	{
		attackIcon.enabled = false;
		powerUpIcon.enabled = false;
		speedUpIcon.enabled = true;
		attackState.enabled = false;
		powerUpState.enabled = false;
		speedUpState.enabled = true;
		attackNumber.enabled = false;
	}

	public void bossGetHurt(int damage)
	{
		bossUnderAttack.enabled = true;
		bossUnderAttackText.enabled = true;
		bossUnderAttackText.text = "-" + damage;
	}

}
