using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTextController : MonoBehaviour
{

	private Text count;

	private Enemy myEnemy;

	// Start is called before the first frame update
	void Start()
    {
		myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		count = gameObject.GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
		count.text = "" + myEnemy.getTimer();
    }
}
