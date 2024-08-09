using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI aggressionText;
    [SerializeField] public TextMeshProUGUI orbCountText;
    [SerializeField] public TextMeshProUGUI gameStateText;

    [SerializeField] public GameObject monster;
    [SerializeField] public GameObject orb;
    [SerializeField] public Transform orbSpawnLocation;
    [SerializeField] public GameObject exit;

    private GameObject[] orbs;
    private int orbCollected = 0;
    private int orbTotal = 0;

    //Direction Lighting = 270.0f to Turn Night Time
    void Start()
    {
        SpawnOrbs(orbSpawnLocation.position.x, orbSpawnLocation.position.z, 10, 10, 10);

        orbs = GameObject.FindGameObjectsWithTag("orb");
        orbTotal = orbs.Length;
    }

    void Update()
    {
        orbs = GameObject.FindGameObjectsWithTag("orb");
        orbCollected = orbTotal - orbs.Length;

        //Code To Update Text
        if (orbCountText != null) { orbCountText.text = "Orb Collected: " + orbCollected + " / " + orbTotal; }
        if (aggressionText != null) {  aggressionText.text = "Aggression: " + Mathf.Floor(monster.GetComponent<AIController>().Aggression) + "%"; }

        //If All Orbs Are Collected Monster Would Start Chase
        if (orbCollected >= orbTotal)
        {
            monster.GetComponent<AIController>().Persistent = true;
            if (orbCountText != null) { orbCountText.text = "RUN!!!"; }
            if (aggressionText != null) { aggressionText.text = "Aggression: " + Mathf.Floor(Random.Range(0.0f,101.0f)) + "%"; }
        }

        //Door Open/Close Coding
        if (exit != null)
        {
            if (orbCollected >= orbTotal && exit.GetComponent<Exit>().DoorOpen == false)
            {
                exit.GetComponent<Exit>().DoorOpen = true;
            }

            if (orbCollected >= orbTotal && exit.GetComponent<Exit>().DoorOpen == true && exit.GetComponent<Exit>().PlayerCollided == true)
            {
                orbCountText.text = "";
                aggressionText.text = "";
                gameStateText.text = "You escaped the labyrinth...";

                Time.timeScale = 0f;
            }
        }

        if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }
    }

    private void SpawnOrbs(float xPos, float zPos, int width, int height, int spacing)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(xPos + (x * spacing), orbSpawnLocation.position.y, zPos + (z * spacing));
                Instantiate(orb, position, Quaternion.identity);
            }
        }
    }
}
