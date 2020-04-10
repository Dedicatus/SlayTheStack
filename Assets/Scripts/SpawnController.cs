
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	Queue<int> spawnQueue = new Queue<int>();
	Queue<int> preQueue = new Queue<int>();

	int[] previewSlot = new int[5];

	int blockRandomizer = -1;
	int attackCount = 3;
	int buffCount = 3;
	int defenseCount = 3;
	int nextBlock = -1;

	[SerializeField] private float dropHeight;
	[SerializeField] private Transform[] towers = new Transform[3];
	[SerializeField] private GameObject[] towerMaterials;
	private float[] towersX = new float[3];
	private float[] towersHeight = new float[3];
	GameObject myMaterial;
	GameObject myMaterialChild;
	


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
		while (spawnQueue.Count < 5)
		{
			fulfillSpawnQueue();
		}

		//copy first spawnQueue Element for UI usage
		spawnQueue.CopyTo(previewSlot, 0);


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

		spawnMaterial();
	}

	void spawnMaterial()
	{
		if (Input.GetKeyUp(KeyCode.F))
		{
			// set the start position of nextblock
			Vector3 startPosition = new Vector3(towers[1].position.x, dropHeight, 0);

			if(myMaterial == null)
			{
				for (int i = 0; i < towers.Length; ++i)
				{
					towersHeight[i] = towers[i].GetComponent<Tower>().getCurHeight();
				}
				nextBlock = spawnQueue.Dequeue();
				fulfillSpawnQueue();
				myMaterial = GameObject.Instantiate(towerMaterials[nextBlock], startPosition, Quaternion.identity);
				
				// refresh the array of spawnQueue elements
				spawnQueue.CopyTo(previewSlot, 0);

			}


			

			//for (int i = 0; i < 5; i++)
			//{
			//	Debug.Log(previewSlot[i]);
			//}

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

	public int[] getSpawnQueueElements()
	{
		return previewSlot;
	}
}
