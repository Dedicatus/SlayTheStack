using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private List<int> myObjectList = new List<int>();

    private int topIndex;

    private float curHeight;

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
            if (myObjectList[myObjectList.Count - topIndex - 2] > 0 && myObjectList[myObjectList.Count - topIndex - 2] <= 4)
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
            Debug.Log("Attack Part");
            for (int i = 1; i <= 3; ++i)
            {
                myObjectList[myObjectList.Count - i] = 4;
            }

            destoryUsedMaterials();
        }
        else if (defCount >= 2)
        {
            Debug.Log("Defense Part");
            for (int i = 1; i <= 3; ++i)
            {
                myObjectList[myObjectList.Count - i] = 5;
            }

            destoryUsedMaterials();
        }
        else if (buffCount >= 2)
        {
            Debug.Log("Buff Part");
            for (int i = 1; i <= 3; ++i)
            {
                myObjectList[myObjectList.Count - i] = 6;
            }

            destoryUsedMaterials();
        }
        else
        {
            Debug.Log("Random Part");
            for (int i = 1; i <= 3; ++i)
            {
                myObjectList[myObjectList.Count - i] = 7;
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
