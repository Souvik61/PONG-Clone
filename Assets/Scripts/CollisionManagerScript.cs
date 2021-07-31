using UnityEngine;
using TMPro;

/// <summary>
/// Collision is managed in collision manager
/// It directly accesses score counters
/// </summary>
public class CollisionManagerScript : MonoBehaviour
{
    public GameManagerNew gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateScore(GameObject caller)
    {
        //Do something
        if (caller.CompareTag("pl_coll"))
        {
          //  enemyScore++;
           // enemyText.text = enemyScore.ToString();
        }
        else if (caller.CompareTag("en_coll"))
        {
           // playerScore++;
           // playerText.text = playerScore.ToString();
        }

    }

    //Call this from outside if any collision occurs
    public void onCollided(GameObject caller)
    {
        gameManager.onCollidedWith(caller);
    }

  
}
