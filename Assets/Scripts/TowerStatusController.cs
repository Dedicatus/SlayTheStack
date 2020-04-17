using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStatusController : MonoBehaviour
{
	int[] towerNumbers = { 0, 0, 0, 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		for( int i = 0; i < towerNumbers.Length; i++)
		{
			GameObject imageObject = transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
			Image iconImage = imageObject.GetComponent<Image>();
			Text iconNumber = imageObject.transform.GetChild(0).GetComponent<Text>();

			if(towerNumbers[i] > 0)
			{
				iconImage.enabled = true;
				iconNumber.enabled = true;
				iconNumber.text = "" + towerNumbers[i];
			}
			else
			{
				iconImage.enabled = false;
				iconNumber.enabled = false;
			}
		}
        
    }

	public void towerIncresed(int index)
	{
		towerNumbers[index] += 1;
	}

	public void towerDecreased(int index)
	{
		towerNumbers[index] -= 1;
	}

}
