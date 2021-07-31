using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    [SerializeField]
    private GameObject refedBall;

    PaddleScript paddleScript;
    private bool aCorRunning = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        paddleScript = GetComponent<PaddleScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (refedBall != null)
        // {
        //     gameObject.transform.position = new Vector3(transform.position.x, refedBall.transform.position.y, 0);

        // }

        TryMove();
       
    }

    //Try move
    private void TryMove()
    {
        if (refedBall == null)
        {
            refedBall = GameObject.FindGameObjectWithTag("Player");
        }

        //If refed ball not equals null
        if (refedBall != null)
        {
            //If ball is at left side
            if (refedBall.transform.position.x <= 2)
            {
                //check if difference between positions can be ignored.
                float diff = Mathf.Abs(transform.position.y - refedBall.transform.position.y);
                if (diff > 0.7f)
                {
                    //If paddle is above the ball
                    if (transform.position.y >= refedBall.transform.position.y)
                    {
                        //Move down
                        if (!aCorRunning)
                        {
                            StartCoroutine(SmoothMove(-1));
                        }
                    }
                    //If paddle is below the ball
                    else if (transform.position.y < refedBall.transform.position.y)
                    {
                        //Move up
                        if (!aCorRunning)
                        {
                            StartCoroutine(SmoothMove(1));
                        }
                    }
                }
            }
        }
    }

    public void UpdateBall(GameObject activeBall)
    {
        refedBall = activeBall;
    }

    private IEnumerator SmoothMove(int dir)
    {
        aCorRunning = true;
        for (int i = 0; i < 8; i++)
        {
            paddleScript.Move(dir);
            yield return null;
        }
        aCorRunning = false;
    }

}
