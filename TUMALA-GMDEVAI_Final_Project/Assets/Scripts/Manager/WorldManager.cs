using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }
    private World world;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            world = new World();
            world.Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public World GetWorld()
    {
        return world;
    }
}
