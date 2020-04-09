using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMaterial : MonoBehaviour
{
    enum MaterialType {Attack, Defense, Buff};

    [SerializeField]
    private MaterialType myType;

    [SerializeField]
    private bool landed;

    private Tower myTowerScript;

    private void Start()
    {
        landed = false;
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
                        break;
                    case MaterialType.Defense:
                        myTowerScript.addMaterial(2);
                        break;
                    case MaterialType.Buff:
                        myTowerScript.addMaterial(3);
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
