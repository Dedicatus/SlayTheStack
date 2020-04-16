using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool gameStart;

    public bool gameSuspended;

    [SerializeField] private int turnCount;

    private Enemy myEnemy;

	private StartScreenTextController myStartTextController;
	private ResultTextController myResultTextController;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        gameSuspended = false;
        turnCount = 0;
        myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
		myStartTextController = GameObject.FindWithTag("System").transform.Find("UIController").transform.GetChild(0).Find("StartScreen").GetChild(0).GetComponent<StartScreenTextController>();
		myResultTextController = GameObject.FindWithTag("System").transform.Find("UIController").transform.GetChild(0).Find("Result").GetChild(0).GetComponent<ResultTextController>();
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
            if (!gameStart)
			{
				gameStart = true;
				myStartTextController.startTextHide();
				myResultTextController.resultTextHide();
			}
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
		myResultTextController.showWin();
        Debug.Log("Game Succeed");
    }

    public void gameFail()
    {
        gameStart = false;
		myResultTextController.showLose();
        Debug.Log("Game Failed");
    }
}
