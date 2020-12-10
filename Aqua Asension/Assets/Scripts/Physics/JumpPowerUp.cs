using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        { 
            other.gameObject.GetComponent<PlayerMove>().jumpForce = 10;
            Destroy(this.gameObject); 
        }
    }
}
