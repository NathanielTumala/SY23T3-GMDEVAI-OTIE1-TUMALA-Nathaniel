using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class World
{
    private GameObject[] hidingSpots;

    public void Initialize()
    {
        RefreshHidingSpots();
    }

    public void RefreshHidingSpots()
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("hide");
    }

    public GameObject[] GetHidingSpots()
    {
        return hidingSpots;
    }
}
