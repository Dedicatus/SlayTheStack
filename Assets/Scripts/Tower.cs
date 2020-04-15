using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private List<int> myObjectList = new List<int>();

    private int topIndex;

	[SerializeField] private int defensePartShield;
    [SerializeField] private float curHeight;

    [SerializeField] private float partYOffset = -7.5f;
    [SerializeField] private float renderDepthOffset = 0.01f;

    [SerializeField] private GameObject attackPartPrefab;
    [SerializeField] private GameObject defensePartPrefab;
    [SerializeField] private GameObject buffPartPrefab;
	//[SerializeField] private GameObject towerShield;

    private GameController myGameController;
	private TowerShield myTowerShieldScript;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();
        topIndex = 0;
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

        int materialCount = 1;
        while (topIndex < 2 && myObjectList.Count >= 3)
        {
            if (myObjectList[myObjectList.Count - topIndex - 2] > 0 && myObjectList[myObjectList.Count - topIndex - 2] <= 3)
            {
                ++materialCount;
            }
            else
            {
                myGameController.turnPassed();
                return;
            }
            ++topIndex;
        }

        if (materialCount >= 2)
        {
            generatePart();
            topIndex = 0;
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
            spawnTowerPart(4);
            destoryUsedMaterials();
        }
        else if (defCount >= 2)
        {
            spawnTowerPart(5);
            destoryUsedMaterials();
        }
        else if (buffCount >= 2)
        {
            spawnTowerPart(6);
            destoryUsedMaterials();
        }
        else
        {
            int ran = Random.Range(0, 3);

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
            
            

            destoryUsedMaterials();
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
        GameObject part = Instantiate(partPrefab, new Vector3(transform.position.x, curHeight + partYOffset, (float)(renderDepthOffset * (myObjectList.Count - 1))), Quaternion.identity);
        part.transform.parent = transform;
        part.GetComponent<TowerPart>().setIndex(myObjectList.Count - 1);
        curHeight += (float)part.GetComponent<BoxCollider>().size.y;
    }

    private void destoryUsedMaterials()
    {
        foreach (Transform matTransform in transform)
        {
            if (matTransform.tag == "TowerMaterial")
            {
                if (matTransform.GetComponent<TowerMaterial>().getIndex() >= myObjectList.Count - 3)
                {
                    curHeight -= (float)matTransform.gameObject.GetComponent<BoxCollider>().size.y;
                    Destroy(matTransform.gameObject);
                }
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
