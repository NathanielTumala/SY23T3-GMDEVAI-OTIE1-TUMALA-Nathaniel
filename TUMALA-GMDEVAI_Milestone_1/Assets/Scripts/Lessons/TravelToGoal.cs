using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToGoal : MonoBehaviour
{
    public Transform goal;
    public float movementSpeed = 5.0f;

    void Start()
    {
        
    }

    void Update()
    {

    }


    void LateUpdate()
    {
        Vector3 direction = goal.position - transform.position;

        transform.LookAt(goal);

        if (direction.magnitude > transform.localScale.x * transform.localScale.z)
        {
            transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);
        }
    }
}
