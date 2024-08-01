using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBounce : MonoBehaviour
{
    public float amplitude = 2.0f;
    public float frequency = 3.0f;
    private float initialY;

    void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        float newY = Mathf.Abs(initialY + (amplitude * Mathf.Sin(Time.time * frequency)));
        transform.position = new Vector3(this.transform.position.x, newY, this.transform.position.z);
    }
}
