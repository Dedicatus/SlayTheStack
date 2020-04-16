using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextController : MonoBehaviour
{
	private Text result;


    // Start is called before the first frame update
    void Start()
    {
		result = gameObject.GetComponent<Text>();
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
	}

	public void showLose()
	{
		result.enabled = true;
		result.text = "You Lose";
		result.color = Color.black;
	}

	public void resultTextHide()
	{
		result.enabled = false;
	}

}
