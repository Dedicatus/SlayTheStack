using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStateController : MonoBehaviour
{
	int[] towerNumbers = { 0, 0, 0, 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void showTower(int index)
	{
		GameObject imageObject = transform.GetChild(0).GetChild(0).GetChild(index).gameObject;
		Image iconImage = imageObject.GetComponent<Image>();
		Text iconNumber = imageObject.transform.GetChild(0).GetComponent<Text>();
		towerNumbers[index] += 1;
		iconImage.enabled = true;
		iconNumber.enabled = true;
		iconNumber.text = "" + towerNumbers[index];
	}
}
