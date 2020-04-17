using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{

	private int currentHealth;
	private int fullHealth;

	private Enemy myEnemy;
	[SerializeField] private Image hpBar;
	[SerializeField] private Text hpNumber;

    // Start is called before the first frame update
    void Start()
    {
		myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		fullHealth = myEnemy.getHealth();
	}

    // Update is called once per frame
    void Update()
    {
		currentHealth = myEnemy.getHealth();

		hpBar.fillAmount = (float) currentHealth / fullHealth;
		hpNumber.text = currentHealth + " / " + fullHealth;
    }
}
