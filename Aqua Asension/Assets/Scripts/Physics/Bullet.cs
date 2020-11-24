using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class Bullet : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;
    private float speed = 100.0f; 

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); 
    }

    public void Initialize(float playerSpeed)
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse); 
    }
    private void OnCollisionEnter(Collision col)
    {
        var enemyPlayerHealth = col.gameObject.GetComponent<Health>(); 
        if(enemyPlayerHealth)
        {
            enemyPlayerHealth.DamageHealth(25);
            Destroy(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject); 
        }
    }

    private void Update()
    {
         
    }
}
