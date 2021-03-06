﻿using System.Collections;
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

    private GameController myGameController;

    private float[] towersX;

    private float[] towersHeight;

    private float[] towersScrolledHeight;

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
        myGameController = GameObject.FindWithTag("System").transform.Find("GameController").GetComponent<GameController>();
        towersX = mySpawnController.getTowersX();
        towersHeight = mySpawnController.getTowersHeight();
        towersScrolledHeight = mySpawnController.getTowersScrolledHeight();
        fallingSpeed = normalSpeed;
        moved = false;
    }

    private void LateUpdate()
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
                if ((transform.position.y > towersHeight[curCol - 1] - towersScrolledHeight[curCol - 1] + transform.localScale.y) && !moved)
                {
                    curCol--;
                    gameObject.transform.position = new Vector3(towersX[curCol], transform.position.y, transform.position.z);
                    moved = true;
                    if (myTowerScript != null) { index = myTowerScript.getCurrentIndex() + 1; }
                    RenderOffset();
                }
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (curCol < 2)
            {
                if ((transform.position.y > towersHeight[curCol + 1] - towersScrolledHeight[curCol + 1] + transform.localScale.y) && !moved)
                {
                    ++curCol;
                    gameObject.transform.position = new Vector3(towersX[curCol], transform.position.y, transform.position.z);
                    moved = true;
                    if (myTowerScript != null) { index = myTowerScript.getCurrentIndex() + 1; }
                    RenderOffset();
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

    private void RenderOffset()
    {
        float offset = 0f;
        if (myTowerScript != null) { offset = myTowerScript.getRenderDepthOffset(); }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, (float)(offset * index));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!landed)
        {
            if (collision.gameObject.tag == "TowerMaterial" || collision.gameObject.tag == "TowerPart" || collision.gameObject.tag == "TowerFloor" || collision.gameObject.tag == "TowerLevel" || collision.gameObject.tag == "TowerBase")
            {
                myGameController.gameSuspended = true;
                landed = true;
                
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                switch (myType)
                {
                    case MaterialType.Attack:
                        myTowerScript.addCurHeight((float)gameObject.GetComponent<BoxCollider>().size.y);
                        myTowerScript.addMaterial(1);
                        break;
                    case MaterialType.Defense:
                        myTowerScript.addCurHeight((float)gameObject.GetComponent<BoxCollider>().size.y);
                        myTowerScript.addMaterial(2);
                        break;
                    case MaterialType.Buff:
                        myTowerScript.addCurHeight((float)gameObject.GetComponent<BoxCollider>().size.y);
                        myTowerScript.addMaterial(3);
                        break;
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TowerArea")
        {
            myTowerScript = other.GetComponent<Tower>();
            if (landed) { index = myTowerScript.getCurrentIndex(); }
            else { index = myTowerScript.getCurrentIndex() + 1; }

            RenderOffset();
            transform.parent = other.transform;
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
