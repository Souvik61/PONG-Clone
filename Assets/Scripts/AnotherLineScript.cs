using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherLineScript : MonoBehaviour
{
    public float xScale;
    public float yScale;

    public Vector2 offset;

    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.material.SetTextureOffset("_MainTex", offset);
        //lineRenderer.material.SetColor("_Color", Color.red);

    }
}
