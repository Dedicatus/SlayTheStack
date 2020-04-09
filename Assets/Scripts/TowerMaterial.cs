using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMaterial : MonoBehaviour
{
    enum MaterialType { Attack, Defense, Buff };

    [SerializeField]
    private MaterialType myType;

    [SerializeField]
    private bool landed;

    private Tower myTowerScript;

    [SerializeField]
    private int curCol;

    private SpawnController mySpawnController;

    private float[] towersX;

    private float[] towersHeight;

    private void Start()
    {
        landed = false;
        curCol = 1;
        mySpawnController = GameObject.FindWithTag("SpawnController").GetComponent<SpawnController>();
        towersX = mySpawnController.getTowersX();
        towersHeight = mySpawnController.getTowersHeight();
    }

    private void Update()
    {
        if (!landed) { inputHandler(); }
    }

    private void inputHandler()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (curCol > 0)
            {
                if (transform.position.y > towersHeight[curCol - 1])
                {
                    curCol--;
                    gameObject.transform.position = new Vector3(towersX[curCol], transform.position.y, transform.position.z);
                }
            }
            else
            {
                return;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (curCol < 2)
            {
                if (transform.position.y > towersHeight[curCol + 1])
                {
                    ++curCol;
                    gameObject.transform.position = new Vector3(towersX[curCol], transform.position.y, transform.position.z);
                }
            }
            else
            {
                return;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!landed)
        {
            if (collision.gameObject.tag == "TowerMaterial" || collision.gameObject.tag == "Tower")
            {
                landed = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                switch (myType)
                {
                    case MaterialType.Attack:
                        myTowerScript.addMaterial(1);
                        myTowerScript.setCurHeight((float)(transform.position.y + transform.localScale.y * 2));
                        break;
                    case MaterialType.Defense:
                        myTowerScript.addMaterial(2);
                        myTowerScript.setCurHeight((float)(transform.position.y + transform.localScale.y * 2));
                        break;
                    case MaterialType.Buff:
                        myTowerScript.addMaterial(3);
                        myTowerScript.setCurHeight((float)(transform.position.y + transform.localScale.y * 2));
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            myTowerScript = other.GetComponent<Tower>();
        }
    }

	public bool isLanded()
	{
		return landed;
	}
}
