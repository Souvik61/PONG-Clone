using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Called from other script
    public void onBallCollided(GameObject caller)
    {
        //Do something
        if (caller.CompareTag("pl_coll"))
        {
            Debug.Log("Player collider");
        }
        else if (caller.CompareTag("en_coll"))
        {
            Debug.Log("Enemy collider");
        }
    }
}
