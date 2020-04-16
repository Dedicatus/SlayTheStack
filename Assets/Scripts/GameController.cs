using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool gameStart;

    public bool gameSuspended;

    [SerializeField] private int turnCount;

    private Enemy myEnemy;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        gameSuspended = false;
        turnCount = 0;
        myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHandler();
    }

    void inputHandler()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (!gameStart) { gameStart = true; }
        }
    }

    public void turnPassed()
    {
        ++turnCount;
        myEnemy.countTurn();
    }

    public void gameSucceed()
    {
        gameStart = false;
        Debug.Log("Game Succeed");
    }

    public void gameFail()
    {
        gameStart = false;
        Debug.Log("Game Failed");
    }
}
