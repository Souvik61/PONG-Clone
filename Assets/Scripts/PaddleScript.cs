using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [Range(1,20)]
    public float speed = 5f;

    public float topLimit;
    public float bottomLimit;

    public void Move(int dir)
    {
        if (dir > 0)
        {
            if (gameObject.transform.position.y < topLimit)
            {
                gameObject.transform.Translate(0, 1 * Time.deltaTime * speed, 0);
            }
        }
        else if (dir < 0)
        {
            if (gameObject.transform.position.y > bottomLimit)
            {
                gameObject.transform.Translate(0, -1 * Time.deltaTime * speed, 0);
            }
        }
    }

}
