using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToDirection : MonoBehaviour
{
    public Vector3 direction = new Vector3 (8.0f, 0.0f, -4.0f);
    public float movementSpeed = 5.0f;

    void Start()
    {
    }

    //Update is for Physics Functions
    void Update()
    {
        
    }

    //LateUpdate is for Movement Functions
    void LateUpdate()
    {
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
    }
}
