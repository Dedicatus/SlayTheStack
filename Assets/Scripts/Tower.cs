using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private List<int> myObjectList = new List<int>();

    private int topIndex;

    private float curHeight;

    [SerializeField] private float partYOffset = -7.5f;

    [SerializeField] private GameObject attackPartPrefab;
    [SerializeField] private GameObject defensePartPrefab;
    [SerializeField] private GameObject buffPartPrefab;

    // Start is called before the first frame update
    void Start()
    {
        topIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCurHeight(float h)
    {
        curHeight = h;
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
                return;
            }
            ++topIndex;
        }

        if (materialCount >= 2)
        {
            generatePart();
            topIndex = 0;
        }  

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
            //Debug.Log("Attack Part");
            for (int i = 1; i <= 3; ++i)
            {
                myObjectList[myObjectList.Count - i] = 4;
            }

            Instantiate(attackPartPrefab, new Vector3(transform.position.x, curHeight + partYOffset, 0), Quaternion.identity);
            destoryUsedMaterials();
        }
        else if (defCount >= 2)
        {
            //Debug.Log("Defense Part");
            for (int i = 1; i <= 3; ++i)
            {
                myObjectList[myObjectList.Count - i] = 5;
            }

            Instantiate(defensePartPrefab, new Vector3(transform.position.x, curHeight + partYOffset, 0), Quaternion.identity);
            destoryUsedMaterials();
        }
        else if (buffCount >= 2)
        {
            //Debug.Log("Buff Part");
            for (int i = 1; i <= 3; ++i)
            {
                myObjectList[myObjectList.Count - i] = 6;
            }

            Instantiate(buffPartPrefab, new Vector3(transform.position.x, curHeight + partYOffset, 0), Quaternion.identity);
            destoryUsedMaterials();
        }
        else
        {

            int ran = Random.Range(0, 3);
            int partType = 0;

            if (ran <= 1)
            {
               // Debug.Log("Random - Attack Part");
                partType = 4;
                for (int i = 1; i <= 3; ++i)
                {
                    myObjectList[myObjectList.Count - i] = partType;
                }
                Instantiate(attackPartPrefab, new Vector3(transform.position.x, curHeight + partYOffset, 0), Quaternion.identity);
            }
            else if (ran <= 2)
            {
                //Debug.Log("Random - Defense Part");
                partType = 5;
                for (int i = 1; i <= 3; ++i)
                {
                    myObjectList[myObjectList.Count - i] = partType;
                }
                Instantiate(defensePartPrefab, new Vector3(transform.position.x, curHeight + partYOffset, 0), Quaternion.identity);
            }
            else
            {
                //Debug.Log("Random - Buff Part");
                partType = 6;
                for (int i = 1; i <= 3; ++i)
                {
                    myObjectList[myObjectList.Count - i] = partType;
                }
                Instantiate(buffPartPrefab, new Vector3(transform.position.x, curHeight + partYOffset, 0), Quaternion.identity);
            }
            
            

            destoryUsedMaterials();
        }
    }

    private void destoryUsedMaterials()
    {
        foreach (Transform matTransform in transform)
        {
            if (matTransform.GetChild(0).tag == "TowerMaterial")
            {
                if (matTransform.GetChild(0).GetComponent<TowerMaterial>().getIndex() >= myObjectList.Count - 3 )
                {
                    Destroy(matTransform.gameObject);
                }
            }
        }
    }

    public int getCurrentIndex()
    {
        return myObjectList.Count - 1;
    }
}
