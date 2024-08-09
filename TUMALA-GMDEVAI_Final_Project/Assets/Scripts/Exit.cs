using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] public GameObject door;
    [SerializeField] public Material closeDoor;
    [SerializeField] public Material openDoor;
    private bool doorOpen = false;
    public bool DoorOpen { get { return doorOpen; } set { doorOpen = value; } }

    private bool playerCollided = false;
    public bool PlayerCollided { get { return playerCollided; } set { playerCollided = value; } }

    void Update()
    {
        if (doorOpen == false)
        {
            Renderer render = door.GetComponent<Renderer>();
            render.material = closeDoor;
        }
        else 
        {
            Renderer render = door.GetComponent<Renderer>();
            render.material = openDoor;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && doorOpen == true)
        {
            playerCollided = true;
        }
    }
}
