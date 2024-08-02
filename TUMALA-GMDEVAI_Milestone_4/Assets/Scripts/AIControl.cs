using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class AIControl : MonoBehaviour
{
    public NavMeshAgent agents;
    public float stoppingDistance;
    public bool consistentFollow = false;

    void Start()
    {
        agents = this.GetComponent<NavMeshAgent>();
        agents.stoppingDistance = stoppingDistance;
    }
}
