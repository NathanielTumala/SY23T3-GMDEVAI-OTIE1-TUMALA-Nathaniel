using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform goal;
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 2.0f;

    private float currentSpeed;

    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.transform.position.x,
                                         this.transform.position.y,
                                         goal.transform.position.z
                                         );

        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                   Quaternion.LookRotation(direction),
                                                   Time.deltaTime * rotationSpeed);

        transform.LookAt(lookAtGoal);

        if (Vector3.Distance(lookAtGoal, transform.position) >
            (transform.localScale.x + transform.localScale.z) -
            (goal.transform.localScale.x + goal.transform.localScale.z))
        {
            currentSpeed = Mathf.Lerp(movementSpeed, movementSpeed * 10, Vector3.Distance(lookAtGoal, transform.position) / 300.0f);

            Debug.Log(currentSpeed);

            transform.Translate(0.0f, 0.0f, currentSpeed * Time.deltaTime);
        }
    }
}
