using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMaterial : MonoBehaviour
{
    enum MaterialType { Attack, Defense, Buff };

    [SerializeField] private MaterialType myType;

    private bool landed;

    private bool moved;

    private Tower myTowerScript;

    private int curCol;

    [SerializeField] private int index;

    private SpawnController mySpawnController;

    private float[] towersX;

    private float[] towersHeight;

    private float fallingSpeed;

    [SerializeField] private float normalSpeed = 20.0f;

    [SerializeField] private float accelerateSpeed = 40.0f;

	[SerializeField] private int health = 20;

    private bool zMoved;

    private void Start()
    {
        landed = false;
        zMoved = false;
        curCol = 1;
        mySpawnController = GameObject.FindWithTag("System").transform.Find("SpawnController").GetComponent<SpawnController>();
        towersX = mySpawnController.getTowersX();
        towersHeight = mySpawnController.getTowersHeight();
        fallingSpeed = normalSpeed;
        moved = false;
    }

    private void Update()
    {
        if (!landed) 
        { 
            inputHandler();
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, -fallingSpeed, 0f);
        }
    }

    private void inputHandler()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (curCol > 0)
            {
                if ((transform.position.y > towersHeight[curCol - 1] + transform.localScale.y) && !moved)
                {
                    curCol--;
                    gameObject.transform.position = new Vector3(towersX[curCol], transform.position.y, transform.position.z);
                    moved = true;
                }
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (curCol < 2)
            {
                if ((transform.position.y > towersHeight[curCol + 1] + transform.localScale.y) && !moved)
                {
                    ++curCol;
                    gameObject.transform.position = new Vector3(towersX[curCol], transform.position.y, transform.position.z);
                    moved = true;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            moved = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            fallingSpeed = normalSpeed;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            fallingSpeed = accelerateSpeed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!landed)
        {
            if (collision.gameObject.tag == "TowerMaterial" || collision.gameObject.tag == "TowerPart" || collision.gameObject.tag == "Tower")
            {
                landed = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                switch (myType)
                {
                    case MaterialType.Attack:
                        myTowerScript.addMaterial(1);
                        myTowerScript.setCurHeight((float)(transform.position.y + transform.localScale.y));
                        break;
                    case MaterialType.Defense:
                        myTowerScript.addMaterial(2);
                        myTowerScript.setCurHeight((float)(transform.position.y + transform.localScale.y));
                        break;
                    case MaterialType.Buff:
                        myTowerScript.addMaterial(3);
                        myTowerScript.setCurHeight((float)(transform.position.y + transform.localScale.y));
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
            index = myTowerScript.getCurrentIndex();
            if (!zMoved)
            {
                transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, (float)(transform.parent.transform.position.z + 0.02 * index));
                zMoved = true;
            }
            transform.parent.parent = other.transform;
        }
    }

	public bool isLanded()
	{
		return landed;
	}

    public int getIndex()
    {
        return index;
    }

	public int getHealth()
	{
		return health;
	}
}
