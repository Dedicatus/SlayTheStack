using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShield : MonoBehaviour
{
	enum ShieldType { Normal, Permanent };

	[SerializeField] private ShieldType myType = ShieldType.Normal;
	[SerializeField] private int currentArmor;

	[SerializeField] private int thornDamage;
	//[SerializeField] private GameObject tower;
	private Tower myTowerScript;
	private TowerScroll myTowerScrollScript;
	private Enemy myEnemyScript;
	private float currentHeight;


	// Start is called before the first frame update
	void Start()
    {
		currentArmor = 0;
		thornDamage = 0;
		if (myType == ShieldType.Normal)
		{
			myTowerScript = transform.parent.GetComponent<Tower>();
		}
		else
		{
			myTowerScript = transform.parent.parent.GetComponent<Tower>();
		}
		myTowerScrollScript = transform.parent.GetComponent<TowerScroll>();
		myEnemyScript = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
	}

    // Update is called once per frame
    void Update()
    {
		if (myType == ShieldType.Normal)
		{
			currentHeight = myTowerScript.getCurHeight() + transform.GetComponent<BoxCollider>().size.y;
			gameObject.transform.localPosition = new Vector3(transform.localPosition.x, currentHeight, transform.localPosition.z);
		}

		if (currentArmor > 0)
		{
			gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
		}
		else
		{
			gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
		}

	}


	public void armorUp(int armorIncrement)
	{
		currentArmor += armorIncrement;
	}
	public int getCurrentArmor()
	{
		return currentArmor;
	}
	public void underAttack(int attack)
	{
		currentArmor -= attack;
	}

	public void addThornDamage(int n)
	{
		thornDamage += n;
	}

	public void reChargeShield()
	{
		if (myType == ShieldType.Permanent)
		{
			currentArmor = myTowerScript.getPermanentShieldAmount();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "EnemyBullet")
		{
			myEnemyScript.underAttack(thornDamage);
		}
	}
}
