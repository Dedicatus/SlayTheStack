using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewController : MonoBehaviour
{
	[SerializeField] private GameObject previewSlot;
	[SerializeField] private GameObject[] newSlot;
	[SerializeField] private Sprite[] towerSprites;
	private SpawnController mySpawnController;
	private int[] spawnQueueElement;

	// Start is called before the first frame update
	void Awake()
    {
		mySpawnController = GameObject.FindWithTag("System").transform.Find("SpawnController").GetComponent<SpawnController>();

		drawPreview();

	}

    // Update is called once per frame
 //   void LateUpdate()
 //   {
	//	if (Input.GetKeyUp(KeyCode.F))
	//	{
	//		refreshPreview();

	//		//for (int i = 0; i < 5; i++)
	//		//{
	//		//	Debug.Log(spawnQueueElement[i]);
	//		//}
	//	}


	//}

	void drawPreview()
	{
		for (int i = 0; i < newSlot.Length; i++)
		{
			newSlot[i] = (GameObject)Instantiate(previewSlot, transform);
		}
	}

	public void refreshPreview()
	{
		spawnQueueElement = mySpawnController.getSpawnQueueElements();

		for (int i = 0; i < newSlot.Length; i++)
		{
			newSlot[i].GetComponent<Image>().sprite = towerSprites[spawnQueueElement[i]];
		}

	}
}
