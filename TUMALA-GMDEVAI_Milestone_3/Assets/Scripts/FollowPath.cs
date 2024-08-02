using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    float movementSpeed = 15.0f;
    float accuracy = 1.0f;
    float rotationSpeed = 15.0f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWaypointIndex = 0;
    Graph graph;

    void Start()
    {
        wps = wpManager.GetComponent<WaypointManager>().waypoints;
        graph = wpManager.GetComponent<WaypointManager>().graph;
        currentNode = wps[0];
    }

    void LateUpdate()
    {
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength()) { return; }

        currentNode = graph.getPathPoint(currentWaypointIndex);

        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position, this.transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex < graph.getPathLength()) 
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.transform.position.x, this.transform.position.y, goal.transform.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                       Quaternion.LookRotation(direction),
                                                       rotationSpeed * Time.deltaTime);
            this.transform.Translate(0.0f, 0.0f, movementSpeed * Time.deltaTime);
        }
    }

    public void GoToWaypoint(int value)
    {
        graph.AStar(currentNode, wps[value]);
        currentWaypointIndex = 0;
    }
}
