using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManagerNew : MonoBehaviour
{
    //Spawned Ball speed
    [Range(5, 20)]
    public int ballSpeed;

    private int enemyScore, playerScore;

    //Score to win game
    private uint scoreToWin;
    //Difficulty of this level
    private uint difficulty;//this adjusts enemy paddle speed

    //Game Texts
    public TMP_Text playerText;
    public TMP_Text enemyText;
    public GameObject gameWinText;
    //Paddles
    public GameObject paddle1, paddle2;
    //Balls
    GameObject activeBall;
    //Spawn ball object
    public BallSpawn ballSpawn;
    //Which player did lose last
    private GameObject currentLoser;

    private enum GameState { RUNNING, PAUSED };
    GameState currentState;


    //Unity Messages----------------------

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        enemyScore = playerScore = 0;
        difficulty = 0;
        activeBall = null;
        currentLoser = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get parameters passed from prev scene
        GetParametersAndAdjustFromData();
        //Start Game
        StartGame();
    }

    // Update is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(EscapeGame());
        }
    }

    //Unity Messages-----------------------end


    //Game events -------------------------start

    //Start game
    void StartGame()
    {
        //Delete ball
        if (activeBall != null) Destroy(activeBall);

        // Reset paddle positions
        paddle1.transform.localPosition = new Vector3(paddle1.transform.localPosition.x, 0, 0);
        paddle2.transform.localPosition = new Vector3(paddle2.transform.localPosition.x, 0, 0);

        StartCoroutine(AskToSpawnBallAtLoc(Vector2.zero, 0));
        currentState = GameState.RUNNING;
    }

    //Restart game
    void RestartGame()
    {
        //Hide wintext
        DisableWinText();
        //Reset Scores
        ResetScores();

        StartGame();

    }

    //Escape game
    IEnumerator EscapeGame()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HomeScene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    //Called when someone wins
    void OnGameOver()
    {
        //If some body won
        if (enemyScore >= scoreToWin)
        {
            EnableWinText(-1);
        }
        else if (playerScore >= scoreToWin)
        {
            EnableWinText(1);
        }
        //Change game state to paused
        currentState = GameState.PAUSED;
    }

    //Any collision occurs
    public void onCollidedWith(GameObject caller)
    {
        //Do something
        if (caller.CompareTag("pl_coll"))
        {
            enemyScore++;
            enemyText.text = enemyScore.ToString();
            currentLoser = paddle2;//Current loser player
                                   //Check score greater than max
            CheckScore();
        }
        else if (caller.CompareTag("en_coll"))
        {
            playerScore++;
            playerText.text = playerScore.ToString();
            currentLoser = paddle1;//Current loser enemy
                                   //Check score greater than max
            CheckScore();

        }
        //Collided with outer border
        else if (caller.CompareTag("p_border"))
        {
            onCollidedWithBounds();
        }
    }

    //Game events ---------------------------end


    //Other functions--------------------start

    void CheckScore()
    {
        //If some body won
        if (enemyScore >= scoreToWin || playerScore >= scoreToWin)
        {
            OnGameOver();
        }
    }

    //Ask spawn manager to spawn a ball (postion on paddles,side L^R)
    IEnumerator AskToSpawnBallAtLoc(Vector2 pos, int side)
    {
        yield return new WaitForSeconds(0.1f);

        activeBall = ballSpawn.SpawnBall(ballSpeed, pos, side);
    }

    //If collided with outer bounds 
    void onCollidedWithBounds()
    {
        Destroy(activeBall);

        //Start a coroutine and ask to spawn a ball in position of current loser;
        //Need to offset position
        if (currentState != GameState.PAUSED)
        {
            var pos = new Vector2(currentLoser.transform.position.x + GetPaddleOffset(currentLoser), currentLoser.transform.position.y);
            StartCoroutine(AskToSpawnBallAtLoc(pos, PaddleToSide(currentLoser)));
        }
        
    }

    //Functions that returns which paddle at which side.
    int PaddleToSide(GameObject paddle)
    {
        //If name=="Paddle 1" return -1 else 1
        return (paddle.name.Equals("Paddle 1")) ? -1 : 1;
    }
    //Paddle specific offse
    float GetPaddleOffset(GameObject paddle)
    {
        return (paddle.name.Equals("Paddle 1")) ? 0.55f : -0.55f;
    }

    //Enable win text
    void EnableWinText(int side)
    {
        int offset = (side < 0) ? -270 : 270;

        var pos = gameWinText.GetComponent<RectTransform>().anchoredPosition;
        gameWinText.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, pos.y);
        gameWinText.SetActive(true);
    }
    //Disable win text
    void DisableWinText()
    {
        gameWinText.SetActive(false);
    }

    //Parse parameters sent from prev scene
    void GetParametersAndAdjustFromData()
    {
        //this.scoreToWin = 1;//Change later
        //difficulty = 2;//Can be 0,1,2

        //Retrieve settings from home scene
        if (GameData.instance != null)
        {
            GameData.ShareData data = GameData.instance.GetSharedData();
            this.scoreToWin = data.highestScore;
            this.difficulty = data.difficulty;
        }

        paddle1.GetComponent<PaddleScript>().speed = DifficultyToSpeedPaddle(difficulty);
        this.ballSpeed = DifficultyToSpeedBall(difficulty);
    }

    private int DifficultyToSpeedPaddle(uint diff)
    {
        int ret = 0;
        switch (diff)
        {
            case 0:
                ret = 5;
                break;
            case 1:
                ret = 7;
                break;
            case 2:
                ret = 11;
                break;
            default:
                break;
        }
        return ret;
    }

    private int DifficultyToSpeedBall(uint diff)
    {
        int ret = 0;
        switch (diff)
        {
            case 0:
                ret = 15;
                break;
            case 1:
                ret = 15;
                break;
            case 2:
                ret = 20;
                break;
            default:
                break;
        }
        return ret;
    }

    private void ResetScores()
    {
        //Set counters to zero
        playerScore = enemyScore = 0;
        playerText.text = "0";
        enemyText.text = "0";
    }

}
