using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    private float health = 100.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        if (health <= 0)
        {
            DoDeath();
        }
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Bullet>().SetInstantiator(this.gameObject);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500.0f);
    }

    public float GetHealth()
    { 
        return health;
    }

        public void SetHealth(float value)
    {
        health = value;
    }

    void DoDeath()
    {
        Destroy(this.gameObject);
    }
}
