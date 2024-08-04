using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public GameObject bullet;
    public GameObject turret;

    public GameObject GetPlayer()
    {
        return player;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            animator.SetFloat("distance", Vector3.Distance(this.transform.position, player.transform.position));
        }
        else
        {
            animator.SetFloat("distance", 999);
        }

        if (GetHealth() <= 0)
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

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }

    public float GetHealth()
    {
        return animator.GetFloat("health");
    }

    public void SetHealth(float value)
    {
        animator.SetFloat("health", value);
    }

    void DoDeath()
    {
        Destroy(this.gameObject);
    }
}
