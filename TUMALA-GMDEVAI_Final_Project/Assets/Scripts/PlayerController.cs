using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource orbAudioSource;
    [SerializeField] AudioClip audioWalk;
    [SerializeField] AudioClip orbAudio;

    public float movementSpeed = 5.0f;

    private Rigidbody rb;
    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 acceleration = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.W)))
        {
            WalkSFX();
            acceleration += transform.forward;
        }
        else if ((Input.GetKey(KeyCode.S)))
        {
            WalkSFX();
            acceleration -= transform.forward;
        }

        if ((Input.GetKey(KeyCode.A)))
        {
            WalkSFX();
            acceleration -= transform.right;
        }

        if ((Input.GetKey(KeyCode.D)))
        {
            WalkSFX();
            acceleration += transform.right;
        }
    }

    void FixedUpdate()
    {
        velocity += acceleration * (movementSpeed / 8.0f);
        velocity = new Vector3(Mathf.Clamp(velocity.x, -movementSpeed, movementSpeed),
                                       Mathf.Clamp(velocity.y, -movementSpeed, movementSpeed),
                                       Mathf.Clamp(velocity.z, -movementSpeed, movementSpeed));
        ApplyMovement(velocity);
        acceleration *= 0.0f;
        velocity = new Vector3(Mathf.Lerp(velocity.x, 0.00f, 0.1f),
                       Mathf.Lerp(velocity.y, 0.00f, 0.1f),
                       Mathf.Lerp(velocity.z, 0.00f, 0.1f));
    }

    private void ApplyMovement(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * Time.deltaTime));
    }
    private void WalkSFX()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = audioWalk;
            audioSource.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("orb"))
        {
            orbAudioSource.clip = orbAudio;
            orbAudioSource.Play();
            Destroy(collision.gameObject);
        }
    }
}
