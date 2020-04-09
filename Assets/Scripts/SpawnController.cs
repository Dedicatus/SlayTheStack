
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	Queue<int> spawnQueue = new Queue<int>();
	Queue<int> preQueue = new Queue<int>();

	int blockRandomizer = -1;
	int redCount = 3;
	int yellowCount = 3;
	int blueCount = 3;
	int nextBlock = -1;

	[SerializeField] private float dropHeight;
	[SerializeField] private Transform[] towerBases;
	[SerializeField] private GameObject[] towerMaterials;
	GameObject myMaterial;
	GameObject myMaterialChild;
	int startColumn = 1;
	static int currentColumn = 1;


	// Start is called before the first frame update
	void Start()
	{
		//initialization bag
		spawnQueue.Clear();
		preQueue.Clear();
		refreshPreQueue();

		//initialize material's attributes
		myMaterial = null;

		//put elements from bag to spawnQueue
		while(spawnQueue.Count < 5)
		{
			fulfillSpawnQueue();
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

		//refulfil bag while bag is empty 
		if (preQueue.Count == 0)
		{
			refreshPreQueue();
		}
		if (myMaterial != null)
		{

			myMaterialChild = myMaterial.transform.GetChild(0).gameObject;
			if (myMaterialChild.GetComponent<TowerMaterial>().isLanded())
			{
				myMaterial = null;
			}

		}
		else
		{
			
		}

		//player control
		drop();
	}

	// player controll
	void drop()
	{
		if (Input.GetKeyUp(KeyCode.F))
		{
			nextBlock = spawnQueue.Dequeue();
			fulfillSpawnQueue();

			// set the start position of nextblock
			Vector3 startPosition = new Vector3(towerBases[startColumn].position.x, dropHeight, 0);

			if(myMaterial == null)
			{
				myMaterial = GameObject.Instantiate(towerMaterials[nextBlock], startPosition, Quaternion.identity);
				currentColumn = startColumn;
			}
			//Debug.Log(preQueue.Count);
			//Debug.Log(spawnQueue.Count);

		}
	}

	void fulfillSpawnQueue()
	{
		int temp = preQueue.Dequeue();
		spawnQueue.Enqueue(temp);
	}

	void refreshPreQueue()
	{
		// reset the count 
		if (redCount == 0 && yellowCount == 0 && blueCount == 0)
		{
			redCount = 3;
			yellowCount = 3;
			blueCount = 3;
		}

		//get random number
		while (redCount > 0 || yellowCount > 0 || blueCount > 0)
		{
			blockRandomizer = Random.Range(0, 3);
			switch (blockRandomizer)
			{
				case 0:
					if(redCount > 0)
					{
						preQueue.Enqueue(blockRandomizer);
						redCount--;
					}
					break;
				case 1:
					if (yellowCount > 0)
					{
						preQueue.Enqueue(blockRandomizer);
						yellowCount--;
					}
					break;
				case 2:
					if(blueCount > 0)
					{
						preQueue.Enqueue(blockRandomizer);
						blueCount--;
					}
					break;
			}
		}

	}
}
