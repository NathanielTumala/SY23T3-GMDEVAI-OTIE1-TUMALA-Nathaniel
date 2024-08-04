using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public GameObject[] goalLocations;
    public NavMeshAgent agent;
    Animator animator;
    float speedMultiplier;
    float detectionRadius = 20.0f;
    float radius = 10.0f;

    void ResetAgent()
    {
        speedMultiplier = Random.Range(0.1f, 1.5f);
        agent.speed = 2.0f * speedMultiplier;
        agent.angularSpeed = 120.0f;
        animator.SetFloat("speedMultiplier", speedMultiplier);
        animator.SetTrigger("isWalking");
        agent.ResetPath();
    }

    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);

        animator.SetTrigger("isWalking");
        animator.SetFloat("wOffSet", Random.Range(0.1f, 1.0f));

        speedMultiplier = Random.Range(0.1f, 1.5f);
        agent.speed = 2.0f * speedMultiplier;
        animator.SetFloat("speedMultiplier", speedMultiplier);
    }

    void LateUpdate()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    public void DetectNewRepeller(Vector3 location)
    {
        if (Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            Vector3 direction = (this.transform.position - location).normalized;
            Vector3 newGoal = this.transform.position + direction * radius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                animator.SetTrigger("isRunning");
                agent.speed = 10.0f;
                agent.angularSpeed = 500.0f;
            }
        }
    }

    public void DetectNewAttractor(Vector3 location)
    {
        if (Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(location, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(location);
                animator.SetTrigger("isRunning");
                agent.speed = 10.0f;
                agent.angularSpeed = 500.0f;
            }

        }
    }
}
