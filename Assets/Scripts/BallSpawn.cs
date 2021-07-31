using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;

    private GameObject activeBall;

    private void Awake()
    {
        activeBall = null;
    }

    //Creates a new ball and returns it.
    public GameObject SpawnBall(int speed, Vector2 pos,int ballSide)
    {
        Destroy(activeBall);//Destroy any prev balls
        var ret = Instantiate(ballPrefab);
        ret.transform.position = pos;//Set position 
        ret.GetComponent<Ball>().velocityVec = GenADirection4(ballSide);
        ret.GetComponent<Ball>().speed = speed;
        activeBall = ret;
        return ret;
    }

    private Vector2 GenADirection()
    {
        bool dir;
        Vector2 dirVec = new Vector2();

        dir = (Random.Range(0.0f, 1.0f) > 0.5f);

        if (dir == true)
        {
            float random = Random.value * 3.1415f + 0.0f;
            dirVec.Set(Mathf.Cos(random), Mathf.Sin(random));
        }
        else
        {
            float random = Random.value * 3.1415f + 0.0f;
            dirVec.Set(Mathf.Cos(random), Mathf.Sin(random));
        }
        return dirVec;
    }

    private Vector2 GenADirection2()
    {
        float r = Mathf.Sqrt(Random.Range(0.0f, 1.0f));
        float theta = Random.Range(0.0f, 1.0f) * 2 * Mathf.PI;

        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);

        return new Vector2(x, y);
    }

    private Vector2 GenADirection3()
    {
        //get direction L or R
        bool dir = (Random.Range(0.0f, 1.0f) > 0.5f);
        float x = dir ? 1 : -1;
        float y = Random.Range(-0.25f, 0.25f);
        return new Vector2(x, y).normalized;
    }

    private Vector2 GenADirection4(int side)
    {
        //get direction L or R
        int dirX = 0;

        if (side == -1 || side == 1)
        {
            dirX = -side;
        } else if (side==0)
        {
            dirX = (Random.Range(0.0f, 1.0f) > 0.5f) ? 1 : -1;
        }
        float y = Random.Range(-0.25f, 0.25f);
        return new Vector2(dirX, y).normalized;
    }

}
