using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public Transform goal;
    public float movementSpeed = 0.0f;
    public float rotationSpeed = 20.0f;
    public float acceleration = 0.5f;
    public float deceleration = 2.0f;
    public float minSpeed = 0.0f;
    public float maxSpeed = 3.0f;
    public float brakeAngle = 20.0f;

    public bool debugAcceleration = false;
    public bool debugAccelerationDetail = false;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 lookAtGoal = new Vector3(goal.transform.position.x,
                                         goal.transform.position.y,  
                                         goal.transform.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                   Quaternion.LookRotation(direction), 
                                                   rotationSpeed * Time.deltaTime);

        if (Vector3.Angle(goal.forward, this.transform.forward) > brakeAngle && movementSpeed > maxSpeed / 4.0f)
        {
            movementSpeed = Mathf.Clamp(movementSpeed - acceleration * Time.deltaTime, minSpeed, maxSpeed);
            if (debugAcceleration == true)
            {
                Debug.Log("Decelerating");
            }
            if (debugAccelerationDetail == true)
            {
                Debug.Log("Decelerating [ANGLE: " + Vector3.Angle(goal.forward, this.transform.forward) + "]");
            }
        }
        else
        {
            movementSpeed = Mathf.Clamp(movementSpeed + acceleration * Time.deltaTime, minSpeed, maxSpeed);
            if (debugAcceleration == true)
            {
                Debug.Log("Accelerating");
            }
            if (debugAccelerationDetail == true)
            {
                Debug.Log("Accelerating [ANGLE: " + Vector3.Angle(goal.forward, this.transform.forward) + "]");
            }
        }
               
        this.transform.Translate(0.0f, 0.0f, movementSpeed);
    }
}
