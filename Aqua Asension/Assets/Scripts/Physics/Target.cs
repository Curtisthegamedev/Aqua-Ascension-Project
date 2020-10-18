using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float health = 30; 
    
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; 
        if(health <= 0)
        {
            Destroy(this.gameObject); 
        }
    }


}
