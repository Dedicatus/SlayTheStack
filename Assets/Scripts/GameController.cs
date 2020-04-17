using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool gameStart;

    public bool gameSuspended;

    [SerializeField] private int turnCount;

    private Enemy myEnemy;

    private SpawnController mySpawnController;
	private StartScreenTextController myStartTextController;
	private ResultTextController myResultTextController;

	private bool isFailed = false;

    public bool isScrolling = false;

    private List<GameObject> myTowerShields = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        gameSuspended = false;
        turnCount = 0;
        myEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        mySpawnController = GameObject.FindWithTag("System").transform.Find("SpawnController").GetComponent<SpawnController>();
        myStartTextController = GameObject.FindWithTag("System").transform.Find("UIController").transform.GetChild(0).Find("StartScreen").GetChild(0).GetComponent<StartScreenTextController>();
		myResultTextController = GameObject.FindWithTag("System").transform.Find("UIController").transform.GetChild(0).Find("Result").GetChild(0).GetComponent<ResultTextController>();

        foreach (GameObject shield in GameObject.FindGameObjectsWithTag("TowerShield"))
        {
            myTowerShields.Add(shield);
        }
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
				myEnemy.gameStartWarning();
			}
			if (isFailed)
			{
				hardRestartGame();
			}
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void turnPassed()
    {
        ++turnCount;
        myEnemy.countTurn();

    }

    public void afterAttack()
    {
        foreach (GameObject shield in myTowerShields)
        {
            shield.GetComponent<TowerShield>().reChargeShield();
        }
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
		isFailed = true;
		myResultTextController.showLose();
        Debug.Log("Game Failed");
    }

	public bool isGameFailed()
	{
		return isFailed;
	}
	public int getCurrentTurn()
	{
		return turnCount;
	}

	private void hardRestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void spwanIfNotScrolling()
    {
        if (gameStart && !gameSuspended)
        {
            mySpawnController.spawnMaterial();
        }
    }
}
