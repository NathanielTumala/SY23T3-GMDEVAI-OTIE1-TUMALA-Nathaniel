using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioEvade;
    [SerializeField] AudioClip audioChase;

    public GameObject target;
    public GameObject lighting;
    public float movementSpeed = 5.0f;
    public bool monsterActionDebug = false;

    private NavMeshAgent agent;
    private PlayerController player;
    private float radius = 25.0f;
    private float aggression = 0.0f;
    public float Aggression {get { return aggression;} set { aggression = value; } }

    private bool isChasing = false;
    private bool isPersistent = false;
    public bool Persistent { get { return isPersistent; } set { isPersistent = value; } }
    private bool isPersistentLighting = false;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;

        if (target != null)
        {
            player = target.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (isPersistent == false)
        {
            if (aggression < 100.0f)
            {
                if (agent.pathPending != true && agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.speed = movementSpeed;
                    agent.acceleration = agent.speed * 0.75f;

                    float movementRandomizer = Random.Range(0.0f, 101.0f);
                    if (movementRandomizer <= 20.0f)
                    {
                        Seek(target.transform.position);
                        if (monsterActionDebug == true) { Debug.Log("Wandering - Player Locate"); }
                    }
                    else if (movementRandomizer <= 50.0f)
                    {
                        GameObject orb = GameObject.FindGameObjectWithTag("orb");
                        if (orb != null) { Seek(orb.transform.position); }
                        if (monsterActionDebug == true) { Debug.Log("Wandering - Orb Locate"); }
                    }
                    else
                    {
                        Wander();
                        if (monsterActionDebug == true) { Debug.Log("Wandering - Wander Locate"); }
                    }
                }

                if (Vector3.Distance(this.transform.position, target.transform.position) < radius)
                {
                    if (audioSource.isPlaying == false)
                    {
                        audioSource.clip = audioEvade;
                        audioSource.Play();
                    }

                    agent.speed = movementSpeed * 2.0f;
                    agent.acceleration = agent.speed * 0.75f;

                    aggression += Random.Range(0.0f, 1.0f);
                    aggression = Mathf.Clamp(aggression, 0.0f, 100.0f);

                    Evade();
                    if (monsterActionDebug == true) { Debug.Log("Evade"); }
                }

            }
            else if (aggression >= 100.0f && (agent.pathPending != true && agent.remainingDistance <= agent.stoppingDistance))
            {
                float chaseRadius = radius * 1.6f; 

                //If Monster Is Chasing And Player Get Out of CHASE Range, Reset.
                if (Vector3.Distance(this.transform.position, target.transform.position) > chaseRadius && isChasing == true)
                {
                    isChasing = false;
                    aggression = 0;

                    if (monsterActionDebug == true) { Debug.Log("Player Escaped"); }
                }
                //Monster Chanses When Inside Original Radius and Can See Player
                else if (CanSeeTarget() && Vector3.Distance(this.transform.position, target.transform.position) < radius)
                {
                    if (audioSource.isPlaying == false)
                    {
                        audioSource.clip = audioChase;
                        audioSource.Play();
                    }

                    isChasing = true;

                    agent.speed = movementSpeed * 2.0f;
                    agent.acceleration = agent.speed * 0.75f;
                    lighting.GetComponent<Light>().enabled = true;

                    Pursue();
                    if (monsterActionDebug == true) { Debug.Log("SEEK!!!"); }
                }
                //Monster Hiding
                else
                {
                    agent.speed = movementSpeed;
                    agent.acceleration = agent.speed * 0.75f;
                    lighting.GetComponent<Light>().enabled = false;

                    CleverHide();
                    if (monsterActionDebug == true) { Debug.Log("Hiding"); }
                }
            }
        }
        else
        {
            if (isPersistentLighting == false)
            {
                lighting.GetComponent<Light>().intensity = 10.0f;
                lighting.GetComponent<Light>().range = 100.0f;
                lighting.transform.position = new Vector3(lighting.transform.position.x + 0.0f,
                                                          lighting.transform.position.y + 64.0f,
                                                          lighting.transform.position.z - 12.0f);
                isPersistentLighting = true;
            }

            if (audioSource.isPlaying == false)
            {
                audioSource.clip = audioChase;
                audioSource.Play();
            }

            isChasing = true;

            agent.speed = movementSpeed * 2.0f;
            agent.acceleration = agent.speed * 0.75f;
            lighting.GetComponent<Light>().enabled = true;

            Pursue();
            if (monsterActionDebug == true) { Debug.Log("SEEK!!!"); }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
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
        float lookAhead = targetDirection.magnitude / (agent.speed + player.movementSpeed);
        Vector3 lookPosition = (target.transform.position + target.transform.forward * lookAhead);
        Seek(lookPosition);
    }

    void Evade()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + player.movementSpeed);
        Vector3 lookPosition = (target.transform.position + target.transform.forward * lookAhead);
        Flee(lookPosition);
    }

    void Wander()
    {
        Vector3 wanderTarget;
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

    void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirection = Vector3.zero;

        World world = WorldManager.Instance.GetWorld();
        GameObject[] hidingSpots = world.GetHidingSpots();
        GameObject chosenGameObject = hidingSpots[0];

        for (int i = 0; i < hidingSpots.Length; i++)
        {
            if (hidingSpots[i] == null)
            {
                world.Initialize();
                return;
            }
        }

        for (int i = 0; i < hidingSpots.Length; i++)
        {
            Vector3 hideDirection = hidingSpots[i].transform.position - target.transform.position;
            Vector3 hidePosition = hidingSpots[i].transform.position - hideDirection.normalized * 5.0f;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDirection = hideDirection;
                chosenGameObject = hidingSpots[i];
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(target.transform.position, this.transform.position);
    }
}
