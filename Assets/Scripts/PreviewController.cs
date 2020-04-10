using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewController : MonoBehaviour
{
	[SerializeField] private GameObject[] previewSlot;
	[SerializeField] private int numberOfSlots;
	[SerializeField] private Sprite[] towerSprites;
	private SpawnController mySpawnController;
	private int[] spawnQueueElement;

	// Start is called before the first frame update
	void Awake()
    {
		mySpawnController = GameObject.FindWithTag("SpawnController").GetComponent<SpawnController>();



	}

    // Update is called once per frame
    void LateUpdate()
    {
		if (Input.GetKeyUp(KeyCode.F))
		{

			drawPreview();

			//for (int i = 0; i < 5; i++)
			//{
			//	Debug.Log(spawnQueueElement[i]);
			//}
		}


	}

	void drawPreview()
	{
		spawnQueueElement = mySpawnController.getSpawnQueueElements();
		GameObject newSlot;
		for (int i = 0; i < numberOfSlots; i++)
		{
			newSlot = (GameObject)Instantiate(previewSlot[spawnQueueElement[i]], transform);
		}
	}


}
