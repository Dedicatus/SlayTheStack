using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShield : MonoBehaviour
{
	[SerializeField] private int currentArmor;
	[SerializeField] private float yOffset;
	//[SerializeField] private GameObject tower;
	private Tower myTowerScript;
	private TowerScroll myTowerScrollScript;
	private float currentHeight;


	// Start is called before the first frame update
	void Start()
    {
		currentArmor = 0;
		myTowerScript = transform.parent.GetComponent<Tower>();
		myTowerScrollScript = transform.parent.GetComponent<TowerScroll>();
	}

    // Update is called once per frame
    void Update()
    {
		currentHeight = myTowerScript.getCurHeight() + transform.GetComponent<BoxCollider>().size.y + yOffset - myTowerScrollScript.getScrolledHeight();
		gameObject.transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

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
}
