using UnityEngine;
using TMPro;

/// <summary>
/// Collision is managed in collision manager
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public TMP_Text playerText;
    public TMP_Text enemyText;

    private int playerScore = 0;
    private int enemyScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(GameObject caller)
    {
        //Do something
        if (caller.CompareTag("pl_coll"))
        {
            enemyScore++;
            enemyText.text = enemyScore.ToString();
        }
        else if (caller.CompareTag("en_coll"))
        {
            playerScore++;
            playerText.text = playerScore.ToString();
        }

    }

    public void onCollided(GameObject caller)
    {
        //Do something
        if (caller.CompareTag("pl_coll"))
        {
            enemyScore++;
            enemyText.text = enemyScore.ToString();
        }
        else if (caller.CompareTag("en_coll"))
        {
            playerScore++;
            playerText.text = playerScore.ToString();
        }
        //Collided with oter border
        else if (caller.CompareTag("p_border"))
        { 
        
        }
    }
}
