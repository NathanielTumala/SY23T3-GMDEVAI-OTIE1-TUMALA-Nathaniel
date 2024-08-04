using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
    public GameObject instantiator;
    private float damage = 10.0f;

    public void SetInstantiator(GameObject instantiator)
    {
        this.instantiator = instantiator;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (instantiator != null)
        {
            //Enemy Shoots Player
            if ((instantiator.tag == "enemy") && (collision.gameObject.tag == "Player"))
            {
                Player hitPlayer = collision.gameObject.GetComponent<Player>();
                collision.gameObject.GetComponent<Player>().SetHealth(hitPlayer.GetHealth() - damage);
                GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(e, 1.5f);
                Destroy(this.gameObject);
            }
            //Player Shoots Enemy
            else if ((instantiator.tag == "Player") && (collision.gameObject.tag == "enemy"))
            {
                //reduce enemy health...
                TankAI hitTank = collision.gameObject.GetComponent<TankAI>();
                collision.gameObject.GetComponent<TankAI>().SetHealth(hitTank.GetHealth() - damage);
                GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(e, 1.5f);
                Destroy(this.gameObject);
            }
            //Bullet Hits Somewhere Else
            else if (collision.gameObject.tag == "Untagged" && collision.gameObject.tag != "bullet")
            {
                GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(e, 1.5f);
                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(e, 1.5f);
            Destroy(this.gameObject);
        }
    }
}
