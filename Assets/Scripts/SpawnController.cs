
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	Queue<int> preview = new Queue<int>();
	Queue<int> bag9 = new Queue<int>();

	int block_randomizer = -1;
	int red_count = 3;
	int yellow_count = 3;
	int blue_count = 3;
	int next_block = -1;

	// Start is called before the first frame update
	void Start()
	{
		//initialization bag
		preview.Clear();
		bag9.Clear();
		bag_generation();

		//put elements from bag to preview
		while(preview.Count < 5)
		{
			fulfill_preview();
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
		if(bag9.Count == 0)
		{
			bag_generation();
		}

		//player control
		drop();
	}

	// player controll
	void drop()
	{
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			next_block = preview.Dequeue();
			fulfill_preview();
			Debug.Log(bag9.Count);
			Debug.Log(preview.Count);

		}
	}

	void fulfill_preview()
	{
		int temp = bag9.Dequeue();
		preview.Enqueue(temp);
	}

	void bag_generation()
	{
		// reset the count 
		if (red_count == 0 && yellow_count == 0 && blue_count == 0)
		{
			red_count = 3;
			yellow_count = 3;
			blue_count = 3;
		}

		//get random number
		while (red_count > 0 || yellow_count > 0 || blue_count > 0)
		{
			block_randomizer = Random.Range(1, 4);
			switch (block_randomizer)
			{
				case 1:
					if(red_count > 0)
					{
						bag9.Enqueue(block_randomizer);
						red_count--;
					}
					break;
				case 2:
					if (yellow_count > 0)
					{
						bag9.Enqueue(block_randomizer);
						yellow_count--;
					}
					break;
				case 3:
					if(blue_count > 0)
					{
						bag9.Enqueue(block_randomizer);
						blue_count--;
					}
					break;
			}
		}

	}
}
