using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private List<int> myObjectList = new List<int>();

    [SerializeField] private int[] topObjects = new int[3];

    private int topIndex;

    [SerializeField] private float curHeight;

    // Start is called before the first frame update
    void Start()
    {
        initiateTopVariables();
    }

    private void initiateTopVariables()
    {
        for (int i = 0; i < topObjects.Length; ++i)
        {
            topObjects[i] = 0;
        }

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
        topObjects[topIndex] = typeCode;
        ++topIndex;

        if (topIndex == topObjects.Length)
        {
            generatePart();
            initiateTopVariables();
        }  

    }

    private void generatePart()
    {
        int atkCount = 0;
        int defCount = 0;
        int buffCount = 0;

        for (int i = 0; i < topObjects.Length; ++i)
        {
            switch (topObjects[i])
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

        }

        if (atkCount >= 2)
        {
            Debug.Log("Attack Part");
        }
        else if (defCount >= 2)
        {
            Debug.Log("Defense Part");
        }
        else if (buffCount >= 2)
        {
            Debug.Log("Buff Part");
        }
        else
        {
            Debug.Log("Random Part");
        }
    }
}
