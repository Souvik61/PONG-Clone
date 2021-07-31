using System.Collections;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Main game manager
/// </summary>
public class GameManagerScript : MonoBehaviour
{
    public TMP_Text playerText;
    public TMP_Text enemyText;
    public GameObject gameWinText;

    public GameObject paddle1, paddle2;
    public GameObject activeBall;
    public BallSpawn ballSpawn;
    public CollisionManagerScript collisionManager;
    public AIPaddle aIPaddle;

    private int playerScore = 0;
    private int enemyScore = 0;

    [Range(0, 20)]
    public int ballSpeed;

    //Score to win
    private uint maxScore;
    private uint difficulty;

    private enum GameState { RUNNING, PAUSED };
    GameState currentState;

    GameObject currentLoser = null;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        maxScore = 10;
        difficulty = 0;//0 refers to easy, 1 medium, 2 hard
    }

    // Start is called before the first frame update
    void Start()
    {
        //Retrieve settings from home scene
        if (GameData.instance != null)
        {
            GameData.ShareData data = GameData.instance.GetSharedData();
            maxScore = data.highestScore;
            SetDiffAccToSett(data.difficulty);
        }

        //Start Game
        Invoke("StartGame", 1f);
    }

    private void SetDiffAccToSett(uint diff)
    {
        switch (diff)
        {
            case 0:
                paddle1.GetComponent<PaddleScript>().speed = 10;
                break;
            case 1:
                paddle1.GetComponent<PaddleScript>().speed = 15;
                break;
            case 2:
                paddle1.GetComponent<PaddleScript>().speed = 20;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.GameReset();
        }
    }

    private void GameReset()
    {
        //Delete ball
        if (activeBall != null) Destroy(activeBall);

        // Reset paddle positions
        paddle1.transform.localPosition = new Vector3(paddle1.transform.localPosition.x, 0, 0);
        paddle2.transform.localPosition = new Vector3(paddle2.transform.localPosition.x, 0, 0);

        DisableWinText();

        ResetScores();

        currentState = GameState.RUNNING;

        //Invoke("AskToSpawnBall", 0.5f);
        StartCoroutine(AskToSpawnBallAtLoc(Vector2.zero, 0));
    }

    void AskToSpwnBall()
    {
        //activeBall = ballSpawn.SpawnBall(ballSpeed);
    }

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
            Destroy(activeBall);

            if (currentState != GameState.PAUSED)
                StartCoroutine(AskToSpawnBallAtLoc(currentLoser.transform.position, PaddleToSide(currentLoser)));
        }
    }

    void CheckScore()
    {
        //If some body won
        if (enemyScore >= maxScore || playerScore >= maxScore)
        {
            onGameOver();
        }
    }

    //Call this to start game
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

    //on Game Over
    void onGameOver()
    {
        //If some body won
        if (enemyScore >= maxScore)
        {
            EnableWinText(-1);
            currentState = GameState.PAUSED;
        }
        else if (playerScore >= maxScore)
        {
            EnableWinText(1);
            currentState = GameState.PAUSED;
        }

    }

    //Reset counters
    public void ResetScores()
    {
        playerScore = enemyScore = 0;
        playerText.text = "0";
        enemyText.text = "0";
    }

    void EnableWinText(int side)
    {
        int offset = (side < 0) ? -270 : 270;

        var pos = gameWinText.GetComponent<RectTransform>().anchoredPosition;
        gameWinText.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, pos.y);
        gameWinText.GetComponent<TextMeshProUGUI>().enabled = true;
    }

    void DisableWinText()
    {
        gameWinText.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    int PaddleToSide(GameObject paddle)
    {
        if (paddle.name.Equals("Paddle 1"))
        {
            return -1;
        }
        return 1;
    }

    IEnumerator AskToSpawnBallAtLoc(Vector2 pos,int side)
    {
        yield return new WaitForSeconds(0.1f);

        activeBall = ballSpawn.SpawnBall(ballSpeed, pos, side);
    }

}