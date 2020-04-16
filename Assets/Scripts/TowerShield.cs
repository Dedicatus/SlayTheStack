using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShield : MonoBehaviour
{
	[SerializeField] private int currentArmor;
	[SerializeField] private float yOffset;

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
		myTowerScript = transform.parent.GetComponent<Tower>();
		myTowerScrollScript = transform.parent.GetComponent<TowerScroll>();
		myEnemyScript = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
	}

    // Update is called once per frame
    void Update()
    {
		currentHeight = myTowerScript.getCurHeight() + transform.GetComponent<BoxCollider>().size.y + yOffset;
		gameObject.transform.localPosition = new Vector3(transform.localPosition.x, currentHeight, transform.localPosition.z);

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

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "EnemyBullet")
		{
			myEnemyScript.underAttack(thornDamage);
		}
	}
}
