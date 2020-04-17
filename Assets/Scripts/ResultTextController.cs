using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextController : MonoBehaviour
{
	private Text result;
	private Text instruction;


    // Start is called before the first frame update
    void Start()
    {
		result = gameObject.GetComponent<Text>();
		instruction = gameObject.transform.parent.GetChild(1).GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void showWin()
	{
		result.enabled = true;
		result.text = "You Win!";
		result.color = Color.red;
		instruction.enabled = true;
		instruction.text = "Press Press Enter to Continue";
	}

	public void showLose()
	{
		result.enabled = true;
		result.text = "You Lose";
		result.color = Color.black;
		instruction.enabled = true;
		instruction.text = "Press Press Enter to Continue";
	}

	public void resultTextHide()
	{
		result.enabled = false;
		instruction.enabled = false;
	}

}
