using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public enum AIBehaviour
    {
        Pursue,
        Hide,
        Evade
    }

    NavMeshAgent agent;
    public GameObject target;
    public WASDMovement playerMovement;

    public float radius = 15.0f;
    public AIBehaviour behaviour;

    Vector3 wanderTarget;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerMovement = target.GetComponent<WASDMovement>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeDirection = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeDirection);
    }

    void Pursue()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currentSpeed);
        Vector3 lookPosition = (target.transform.position + target.transform.forward * lookAhead);
        Seek(lookPosition);
    }

    void Evade()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currentSpeed);
        Vector3 lookPosition = (target.transform.position + target.transform.forward * lookAhead);
        Flee(lookPosition);
    }

    void Wander()
    {
        float wanderRadius = 20.0f;
        float wanderDistance = 10.0f;
        float wanderJitter = 1.0f;

        wanderTarget = new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                   0.0f,
                                   Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0.0f, 0.0f, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5.0f;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                distance = spotDistance;
            }
        }

        Seek(chosenSpot);
    }

    void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirection = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5.0f;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDirection = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }

        Collider hideCollider = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, chosenDirection.normalized);
        RaycastHit info;
        float rayDistance = 100.0f;
        hideCollider.Raycast(back, out info, rayDistance);

        Seek(info.point + chosenDirection.normalized * 5.0f);
    }

    bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            return raycastInfo.transform.gameObject.tag == "Player";
        }
        return false;
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position) < radius)
        {
            if (behaviour == AIBehaviour.Pursue)
            {
                Pursue();
            }
            else if (behaviour == AIBehaviour.Hide)
            {
                if (CanSeeTarget())
                {
                    CleverHide();
                }
            }
            else if (behaviour == AIBehaviour.Evade)
            {
                Evade();
            }
        }
        else
        {
            Wander();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}

