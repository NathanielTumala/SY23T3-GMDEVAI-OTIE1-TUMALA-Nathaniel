using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;
    public Transform player;

    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    void Update()
    {
        foreach (GameObject ai in agents)
        {
            if (ai.GetComponent<AIControl>().consistentFollow == false)
            {
                if (Input.GetMouseButton(0))
                {
                    ai.GetComponent<AIControl>().agents.SetDestination(player.transform.position);
                }
            }
            else
            {
                ai.GetComponent<AIControl>().agents.SetDestination(player.transform.position);
            }
        }
            /*      
                    if (Input.GetMouseButtonDown(0))
                    {
                        RaycastHit hit;

                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
                        {
                            foreach (GameObject ai in agents)
                            {
                                ai.GetComponent<AIControl>().agents.SetDestination(hit.point);
                            }

                        }
                    }
            */

        }
}
