using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    //public GameObject[] waypoints;
    public UnityStandardAssets.Utility.WaypointCircuit circuit;
    private int currentWaypointIndex = 0;

    public float movementSpeed = 5.0f;
    public float rotationSpeed = 3.0f;
    public float accuracy = 1.0f;


    void Start()
    {
        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    void LateUpdate()
    {
        if (circuit.Waypoints.Length == 0) return;

        GameObject currentWaypoint = circuit.Waypoints[currentWaypointIndex].gameObject;
        Vector3 lookAtGoal = new Vector3(currentWaypoint.transform.position.x,
                                         this.transform.position.y,
                                         currentWaypoint.transform.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;


        if (direction.magnitude < 1.0f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= circuit.Waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        else
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            this.transform.Translate(0.0f, 0.0f, movementSpeed * Time.deltaTime);
        }
    }
}
