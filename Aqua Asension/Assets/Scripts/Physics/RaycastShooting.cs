using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooting : MonoBehaviour
{
    private float damage = 10;
    private float rangeOfWeapon = 100;
    [SerializeField] Transform shootSpot;  

    private void shoot()
    {

    }
    private void Update()
    {
        //shoots a raycast if the player left clicks
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("button clicked"); 
            RaycastHit hit;
            if(Physics.Raycast(shootSpot.position, shootSpot.forward, out hit, rangeOfWeapon))
            {
                Debug.Log(hit.transform.name); 
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                Vector3 dir = transform.TransformDirection(Vector3.forward) * rangeOfWeapon;
            }
        }
    }
}
