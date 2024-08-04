using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorSpawn : MonoBehaviour
{
    public GameObject repeller;
    public GameObject attractor;
    GameObject[] agents;

    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.point.y < 0.5 && hit.point.y > -0.5)
                {
                    Instantiate(repeller, hit.point, repeller.transform.rotation);
                    foreach (GameObject a in agents)
                    {
                        a.GetComponent<AIControl>().DetectNewRepeller(hit.point);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.point.y < 0.5 && hit.point.y > -0.5)
                {
                    Instantiate(attractor, hit.point, attractor.transform.rotation);
                    foreach (GameObject a in agents)
                    {
                        a.GetComponent<AIControl>().DetectNewAttractor(hit.point);
                    }
                }
            }
        }
    }
}
