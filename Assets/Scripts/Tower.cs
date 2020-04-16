using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private List<int> myObjectList = new List<int>();

    private int searchIndex;

	[SerializeField] private int defensePartShield;
    [SerializeField] private float curHeight;

    [SerializeField] private float renderDepthOffset = 0.01f;

    [SerializeField] private GameObject attackPartPrefab;
    [SerializeField] private GameObject defensePartPrefab;
    [SerializeField] private GameObject buffPartPrefab;
	[SerializeField] private GameObject advAttackLevelPrefab;
	[SerializeField] private GameObject advDefenseLevelPrefab;
	[SerializeField] private GameObject advBuffLevelPrefab;
	[SerializeField] private GameObject atkDefenseLevelPrefab;
	[SerializeField] private GameObject atkBuffLevelPrefab;
	[SerializeField] private GameObject defBuffLevelPrefabl;

	//[SerializeField] private GameObject towerShield;

    private GameController myGameController;
	private TowerShield myTowerShieldScript;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();
        searchIndex = 0;
        curHeight = transform.GetChild(0).transform.localScale.y;

		myTowerShieldScript = gameObject.transform.Find("TowerShield").GetComponent<TowerShield>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addCurHeight(float h)
    {
        curHeight += h;
    }

    public float getCurHeight()
    {
        return curHeight;
    }

    public void addMaterial(int typeCode)
    {
        myObjectList.Add(typeCode);
		// check two blocks below the current one
        int materialCount = 1;
        while (searchIndex < 2 && myObjectList.Count >= 3)
        {
            if (myObjectList[myObjectList.Count - searchIndex - 2] > 0 && myObjectList[myObjectList.Count - searchIndex - 2] <= 3)
            {
                ++materialCount;
            }
            else
            {
                myGameController.turnPassed();
                return;
            }
            ++searchIndex;
			
		}

        if (materialCount >= 2)
        {
            generatePart();
			//check the block below current part 
			if(myObjectList.Count >= 6 && myObjectList[myObjectList.Count - 4] >= 4 && myObjectList[myObjectList.Count - 4] <= 6)
			{
				generateLevel();
			}

            searchIndex = 0;
        }

        myGameController.turnPassed();
    }

    private void generatePart()
    {
        if (myObjectList.Count < 3) { return; }
        int atkCount = 0;
        int defCount = 0;
        int buffCount = 0;

        for (int i = 1; i <= 3; ++i)
        {
            switch (myObjectList[myObjectList.Count - i])
            {
                case 1:
                    ++atkCount;
                    break;
                case 2:
                    ++defCount;
                    break;
                case 3:
                    ++buffCount;
                    break;
                default:
                    break;
            }
            myObjectList[myObjectList.Count - i] = 0;
        }

        if (atkCount >= 2)
        {
			destoryUsedMaterials();
			spawnTowerPart(4);

        }
        else if (defCount >= 2)
        {
			destoryUsedMaterials();
			spawnTowerPart(5);

        }
        else if (buffCount >= 2)
        {
			destoryUsedMaterials();
			spawnTowerPart(6);

        }
        else
        {
            int ran = Random.Range(0, 3);
			destoryUsedMaterials();
			if (ran <= 1)
            {
                spawnTowerPart(4);
            }
            else if (ran <= 2)
            {
                spawnTowerPart(5);
            }
            else
            {
                spawnTowerPart(6);
            }

        }
    }

    private void spawnTowerPart(int type)
    {
        for (int i = 1; i <= 3; ++i)
        {
            myObjectList[myObjectList.Count - i] = type;
        }

        GameObject partPrefab;
        switch (type)
        {
            case 4:
                partPrefab = attackPartPrefab;
                break;
            case 5:
                partPrefab = defensePartPrefab;
				myTowerShieldScript.armorUp(defensePartShield);
                break;
            case 6:
                partPrefab = buffPartPrefab;
                break;
            default:
                partPrefab = null;
                Debug.LogError("Invalid Type Code");
                break;
        }

        Debug.Log("curHeight: " + curHeight + "size.y / 2: " + (float)((partPrefab.transform.GetChild(0).GetComponent<BoxCollider>().size.z * partPrefab.transform.GetChild(0).transform.localScale.z) / 2.0f));

        GameObject part = Instantiate(partPrefab, new Vector3(transform.position.x, curHeight + (float)(((partPrefab.transform.GetChild(0).GetComponent<BoxCollider>().size.z * partPrefab.transform.GetChild(0).transform.localScale.z) / 2.0f) - partPrefab.GetComponent<BoxCollider>().center.y), (float)(renderDepthOffset * (myObjectList.Count - 1))), Quaternion.identity);
        part.transform.parent = transform;
        part.GetComponent<TowerPart>().setIndex(myObjectList.Count - 1);
        curHeight += (float)(partPrefab.transform.GetChild(0).GetComponent<BoxCollider>().size.z * partPrefab.transform.GetChild(0).transform.localScale.z);
        //GameObject part = Instantiate(partPrefab, new Vector3(transform.position.x, curHeight + partYOffset, (float)(renderDepthOffset * (myObjectList.Count - 1))), Quaternion.identity);
        //part.transform.parent = transform;
        //part.GetComponent<TowerPart>().setIndex(myObjectList.Count - 1);
        //curHeight += (float)part.GetComponent<BoxCollider>().size.y;
    }

	private void generateLevel()
	{
		if (myObjectList.Count < 6) { return; }

		int upperPartType = myObjectList[myObjectList.Count - 1];
		int lowerPartType = myObjectList[myObjectList.Count - 4];
		//16 -> 4 * 4 -> atk & atk = adv atk -> 7
		//20 -> 4 * 5 -> atk & defense = atk def -> 10
		//24 -> 4 * 6 -> atk & buff = atk buff -> 11
		//25 -> 5 * 5 -> def & def = adv def -> 8
		//30 -> 5 * 6 -> def & buff = def buff -> 12
		//36 -> 6 * 6 -> buff & buff = adv buff -> 9
		switch (upperPartType * lowerPartType)
		{
			case 16:
				destoryUsedLevels();
				spawnTowerLevel(7);

				break;
			case 20:
				destoryUsedLevels();
				spawnTowerLevel(10);

				break;
			case 24:
				destoryUsedLevels();
				spawnTowerLevel(11);

				break;
			case 25:
				destoryUsedLevels();
				spawnTowerLevel(8);

				break;
			case 30:
				destoryUsedLevels();
				spawnTowerLevel(12);

				break;
			case 36:
				destoryUsedLevels();
				spawnTowerLevel(9);

				break;
			default:
				break;
		}


	}

	private void spawnTowerLevel(int type)
	{
		for (int i = 1; i <= 6; ++i)
		{
			myObjectList[myObjectList.Count - i] = type;
		}
		GameObject partPrefab;
		switch (type)
		{
			case 7:
				partPrefab = advAttackLevelPrefab;
				break;
			case 8:
				partPrefab = advDefenseLevelPrefab;
				break;
			case 9:
				partPrefab = advBuffLevelPrefab;
				break;
			case 10:
				partPrefab = atkDefenseLevelPrefab;
				break;
			case 11:
				partPrefab = atkBuffLevelPrefab;
				break;
			case 12:
				partPrefab = defBuffLevelPrefabl;
				break;

			default:
				partPrefab = null;
				Debug.LogError("Invalid Type Code");
				break;
		}
		GameObject part = Instantiate(partPrefab, new Vector3(transform.position.x, curHeight + partPrefab.GetComponent<BoxCollider>().size.y / 2, (float)(renderDepthOffset * (myObjectList.Count - 1))), Quaternion.identity);
		part.transform.parent = transform;
		part.GetComponent<TowerLevel>().setIndex(myObjectList.Count - 1);
		curHeight += (float)part.GetComponent<BoxCollider>().size.y;
	}


    private void destoryUsedMaterials()
    {
        foreach (Transform matTransform in transform)
        {
            if (matTransform.tag == "TowerMaterial")
            {
				curHeight -= (float)matTransform.gameObject.GetComponent<BoxCollider>().size.y;
				Destroy(matTransform.gameObject);
				//if (matTransform.GetComponent<TowerMaterial>().getIndex() >= myObjectList.Count - 3)
    //            {
    //                curHeight -= (float)matTransform.gameObject.GetComponent<BoxCollider>().size.y;
    //                Destroy(matTransform.gameObject);
    //            }
            }
            else
            {
                continue;
            }

        }
	}
	private void destoryUsedLevels()
	{
		foreach (Transform matTransform in transform)
		{
			if (matTransform.tag == "TowerPart")
			{
				curHeight -= (float)matTransform.gameObject.GetComponent<BoxCollider>().size.y;
				Destroy(matTransform.gameObject);

			}
			else
			{
				continue;
			}

		}
	}



    public int getCurrentIndex()
    {
        return myObjectList.Count;
    }

	public void listRemoveElement()
	{
		int currentSize = myObjectList.Count;
		myObjectList.RemoveAt(currentSize - 1);
	}

    public float getRenderDepthOffset()
    {
        return renderDepthOffset;
    }

}
