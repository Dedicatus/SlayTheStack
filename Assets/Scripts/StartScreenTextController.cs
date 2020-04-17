using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenTextController : MonoBehaviour
{
	private Text start;

    // Start is called before the first frame update
    void Start()
    {
        start = gameObject.GetComponent<Text>();
		startTextShow();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void startTextShow()
	{
		start.enabled = true;
		start.text = "Press Enter to Start";
	}
	public void startTextHide()
	{
		start.enabled = false;
	}
}
