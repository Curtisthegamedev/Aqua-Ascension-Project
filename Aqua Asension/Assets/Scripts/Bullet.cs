using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 3.0f; 

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); 
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag != "Player")
        {
            Destroy(this.gameObject); 
        }
    }

    private void Update()
    {
        rb.AddForce(transform.forward * speed); 
    }
}
