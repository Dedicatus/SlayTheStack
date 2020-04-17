
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	Queue<int> spawnQueue = new Queue<int>();
	Queue<int> preQueue = new Queue<int>();

	int[] previewQueueCopy = new int[3];

	int blockRandomizer = -1;
	int attackCount = 3;
	int buffCount = 3;
	int defenseCount = 3;
	int nextBlock = -1;
	bool isCounting;

	[SerializeField] private float spawnWaitTime = 0.5f;
	[SerializeField] private float spawnWaitTimer = 0.0f;
	[SerializeField] private float dropHeight;
	[SerializeField] private Transform[] towers = new Transform[3];
	[SerializeField] private GameObject attackMaterials;
	[SerializeField] private GameObject buffMaterials;
	[SerializeField] private GameObject deffenseMaterials;

	private float[] towersX = new float[3];
	private float[] towersHeight = new float[3];
	private float[] towersScrolledHeight = new float[3];
	[SerializeField] GameObject myMaterial;
	GameObject myMaterialChild;

	private GameObject[] towerMaterials = new GameObject[3];

	private PreviewController myPreviewController;

	private GameController myGameController;

	

	// Start is called before the first frame update
	void Start()
	{
		myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();

		myPreviewController = GameObject.FindWithTag("System").transform.Find("UIController").transform.Find("UI-Screen").transform.Find("Preview").GetComponent<PreviewController>();

		towerMaterials[0] = attackMaterials;
		towerMaterials[1] = buffMaterials;
		towerMaterials[2] = deffenseMaterials;

		//initialization bag
		spawnQueue.Clear();
		preQueue.Clear();
		refreshPreQueue();

		//initialize material's attributes
		myMaterial = null;

		//put elements from bag to spawnQueue
		while (spawnQueue.Count < 3)
		{
			fillSpawnQueue();
		}

		//copy first spawnQueue Element for UI usage
		spawnQueue.CopyTo(previewQueueCopy, 0);


		for (int i = 0; i < towers.Length; ++i)
		{
			towersX[i] = (float)towers[i].position.x;
			towersHeight[i] = 0f;
		}

	}

	// Update is called once per frame
	void Update()
	{
		//if (preQueue.Count != 0)
		//{
		//	int test = preQueue.Dequeue();
		//	Debug.Log(test);
		//}

		//refill bag while bag is empty 
		if (preQueue.Count == 0)
		{
			refreshPreQueue();
		}

		if (myMaterial != null)
		{
			if (myMaterial.GetComponent<TowerMaterial>().isLanded())
			{
				if(spawnWaitTimer<=0)
					spawnWaitTimer = spawnWaitTime;
			}
		}
		if (spawnWaitTimer>0)
		{
			spawnWaitTimer -= Time.deltaTime;
			if(spawnWaitTimer<=0)
				myMaterial = null;
		}

		/*
		if (myGameController.gameStart) 
		{ 
			if (!myGameController.gameSuspended)
			{ 
				spawnMaterial();
			}
		}
		*/
	}

	public void spawnMaterial()
	{
		// set the start position of nextblock
		Vector3 startPosition = new Vector3(towers[1].position.x, dropHeight, 0);

		if (myMaterial == null)
		{
			for (int i = 0; i < towers.Length; ++i)
			{
				towersHeight[i] = towers[i].GetComponent<Tower>().getCurHeight();
				towersScrolledHeight[i] = towers[i].GetComponent<TowerScroll>().getScrolledHeight();
			}
			nextBlock = spawnQueue.Dequeue();
			fillSpawnQueue();
			myMaterial = GameObject.Instantiate(towerMaterials[nextBlock], startPosition, Quaternion.identity);

			// refresh the array of spawnQueue elements
			spawnQueue.CopyTo(previewQueueCopy, 0);

			myPreviewController.refreshPreview();

		}
	}

	void fillSpawnQueue()
	{
		int temp = preQueue.Dequeue();
		spawnQueue.Enqueue(temp);
	}

	void refreshPreQueue()
	{
		// reset the count 
		if (attackCount == 0 && buffCount == 0 && defenseCount == 0)
		{
			attackCount = 3;
			buffCount = 3;
			defenseCount = 3;
		}

		//get random number
		while (attackCount > 0 || buffCount > 0 || defenseCount > 0)
		{
			blockRandomizer = Random.Range(0, 3);
			switch (blockRandomizer)
			{
				case 0:
					if(attackCount > 0)
					{
						preQueue.Enqueue(blockRandomizer);
						attackCount--;
					}
					break;
				case 1:
					if (buffCount > 0)
					{
						preQueue.Enqueue(blockRandomizer);
						buffCount--;
					}
					break;
				case 2:
					if(defenseCount > 0)
					{
						preQueue.Enqueue(blockRandomizer);
						defenseCount--;
					}
					break;
			}
		}

	}

	public float[] getTowersX()
	{
		return towersX;
	}

	public float[] getTowersHeight()
	{
		return towersHeight;
	}

	public float[] getTowersScrolledHeight()
	{
		return towersScrolledHeight;
	}

	public int[] getSpawnQueueElements()
	{
		return previewQueueCopy;
	}
}
