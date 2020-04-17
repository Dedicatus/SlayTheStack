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
		result.color = new Color(255.0f / 255.0f, 83.0f / 255.0f, 83.0f / 255.0f);
		instruction.enabled = true;
		instruction.text = "Press Enter to Continue";
	}

	public void showLose()
	{
		result.enabled = true;
		result.text = "You Lose";
		result.color = new Color(173.0f / 255.0f, 82.0f / 255.0f, 37.0f / 255.0f);
		instruction.enabled = true;
		instruction.text = "Press Enter to Continue";
	}

	public void resultTextHide()
	{
		result.enabled = false;
		instruction.enabled = false;
	}

}
