using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectScript : MonoBehaviour
{
    //public CollisionScript collisionScript;
    public CollisionManagerScript collManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collisionScript.onBallCollided(this.gameObject);
        collManager.onCollided(this.gameObject);
    }

}
