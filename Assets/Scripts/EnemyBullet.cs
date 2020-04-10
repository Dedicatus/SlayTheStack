using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

	private Enemy myEnemy;
	[SerializeField] private float dropSpeed = 40.0f;


	// Start is called before the first frame update
	void Start()
    {
        myEnemy = GameObject.FindWithTag("System").transform.Find("EnemyController").GetComponent<Enemy>();
	}

    // Update is called once per frame
    void Update()
    {
		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, -dropSpeed, 0f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "TowerMaterial")
		{
			Debug.Log("collided");
			Destroy(collision.gameObject.transform.parent.gameObject);
		}

		if (collision.gameObject.tag == "Tower")
		{
			Destroy(transform.parent.gameObject);
			myEnemy.resetAttackMaterial();
		}
	}
}
