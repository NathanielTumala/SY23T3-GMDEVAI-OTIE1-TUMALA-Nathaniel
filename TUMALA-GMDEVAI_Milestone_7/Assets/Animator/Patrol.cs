using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM
{
    GameObject[] waypoints;
    int currentWaypoint;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWaypoint = Random.Range(0, waypoints.Length);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (waypoints.Length == 0) { return; }

        if (Vector3.Distance(waypoints[currentWaypoint].transform.position, NPC.transform.position) < accuracy)
        {
            currentWaypoint = Random.Range(0, waypoints.Length);
        }

        var direction = waypoints[currentWaypoint].transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  rotationSpeed * Time.deltaTime );
        NPC.transform.Translate(0.0f, 0.0f, Time.deltaTime * movementSpeed);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
