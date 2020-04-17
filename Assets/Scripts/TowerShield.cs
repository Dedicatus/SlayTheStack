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

	private Transform myTansform;
	private GameObject myTowerShield;
	private float offset = 0f;


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
			myTowerShield = transform.parent.gameObject;
			//offset = transform.localPosition.y - myTowerShield.transform.localPosition.y;
			offset = -1.0f;
			myTansform = transform;
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

		if (myType == ShieldType.Permanent)
		{
			if (myTowerShield.GetComponent<TowerShield>().getCurrentArmor() == 0)
			{
				transform.localPosition = new Vector3(0f, 0f, 0f);
			}
			else
			{
				transform.localPosition = new Vector3(0f, -1, 0f);
			}
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
