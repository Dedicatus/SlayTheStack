
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	Queue<int> spawnQueue = new Queue<int>();
	Queue<int> preQueue = new Queue<int>();

	int redCount = 3;
	int yellowCount = 3;
	int blueCount = 3;
	int nextBlock = -1;

	// Start is called before the first frame update
	void Start()
	{
		//initialization bag
		spawnQueue.Clear();
		preQueue.Clear();
		refreshPreQueue();

		//put elements from bag to spawnQueue
		while(spawnQueue.Count < 5)
		{
			fulfillSpawnQueue();
		}
	}

	// Update is called once per frame
	void Update()
	{
		//if(bag9.Count != 0)
		//{
		//	int test = bag9.Dequeue();
		//	Debug.Log(test);
		//}
		
		//refulfil bag while bag is empty 
		if(preQueue.Count == 0)
		{
			refreshPreQueue();
		}

		//player control
		drop();
	}

	// player controll
	void drop()
	{
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			//check prequeue 
			if(preQueue.Count == 0)
			{
				refreshPreQueue();
			}
			nextBlock = spawnQueue.Dequeue();
			fulfillSpawnQueue();
			Debug.Log(preQueue.Count);
			Debug.Log(spawnQueue.Count);

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
			int block_randomizer = Random.Range(1, 4);
			switch (block_randomizer)
			{
				case 1:
					if(redCount > 0)
					{
						preQueue.Enqueue(block_randomizer);
						redCount--;
					}
					break;
				case 2:
					if (yellowCount > 0)
					{
						preQueue.Enqueue(block_randomizer);
						yellowCount--;
					}
					break;
				case 3:
					if(blueCount > 0)
					{
						preQueue.Enqueue(block_randomizer);
						blueCount--;
					}
					break;
			}
		}

	}
}
