using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackWarningController : MonoBehaviour
{
	[SerializeField] private float height;
	private Enemy myEnemy;
	private SpawnController mySpawnController;
	private Image warningImage;
	float[] towerX = new float[3];

	// Start is called before the first frame update
	void Start()
    {
		myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		mySpawnController = GameObject.FindWithTag("System").transform.Find("SpawnController").GetComponent<SpawnController>();
		towerX = mySpawnController.getTowersX();
		warningImage = gameObject.GetComponent<Image>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	public void nextAttackWarning(int index)
	{
		gameObject.transform.position = new Vector3(towerX[index], height, transform.position.z);
	}
	public void isImageDisplay(bool isDisplay)
	{
		warningImage.enabled = isDisplay;
	}
}
