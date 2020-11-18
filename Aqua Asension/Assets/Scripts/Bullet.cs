using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class Bullet : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;
    private float speed = 3.0f; 

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); 
    }
    private void OnCollisionEnter(Collision col)
    {
            PhotonView view;  
            view = col.gameObject.GetComponent<PhotonView>(); 
            if(!view.IsMine && col.gameObject.tag == "Player")
            {
            col.gameObject.GetComponent<PlayerMove>().StunDamage(1); 
            }
        Destroy(this.gameObject); 
    }

    private void Update()
    {
        rb.AddForce(transform.forward * speed); 
    }
}
