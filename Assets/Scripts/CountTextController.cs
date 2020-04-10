using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTextController : MonoBehaviour
{

	private Text count;

	private GameController myGameController;

	// Start is called before the first frame update
	void Start()
    {
		myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();
		count = gameObject.GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
		count.text = "" + myGameController.getCount();
    }
}
