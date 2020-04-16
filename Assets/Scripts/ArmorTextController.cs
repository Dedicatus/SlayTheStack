using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorTextController : MonoBehaviour
{
	private Text armor;
	private int currentArmor;
	private TowerShield myTowerShieldScript;


	// Start is called before the first frame update
	void Start()
    {
		myTowerShieldScript = transform.parent.transform.parent.GetComponent<TowerShield>();
		armor = gameObject.GetComponent<Text>(); 
	}

    // Update is called once per frame
    void Update()
    {
		currentArmor = myTowerShieldScript.getCurrentArmor();
		armor.text = "" + currentArmor;
		if(currentArmor > 0)
		{
			armor.enabled = true;
		}
		else
		{
			armor.enabled = false;
		}

	}
}
