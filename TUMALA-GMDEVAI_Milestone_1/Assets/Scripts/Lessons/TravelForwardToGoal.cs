using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelForwardToGoal : MonoBehaviour
{
    public Transform goal;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 4.0f;

    void Start()
    {
        
    }


    void LateUpdate()
    {
        Vector3 lookAtGoat = new Vector3(goal.transform.position.x,
                                         this.transform.position.y,
                                         goal.transform.position.z
                                         );

        Vector3 direction = lookAtGoat - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                   Quaternion.LookRotation(direction),
                                                   Time.deltaTime * rotationSpeed);

        transform.LookAt(lookAtGoat);

        if (Vector3.Distance(lookAtGoat, transform.position) > transform.localScale.x * transform.localScale.z)
        {
            transform.Translate(0.0f, 0.0f, movementSpeed * Time.deltaTime);
        }
    }
}
