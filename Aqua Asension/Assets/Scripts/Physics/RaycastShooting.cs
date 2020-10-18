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
        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if(Physics.Raycast(shootSpot.position, shootSpot.forward, out hit, rangeOfWeapon))
            {
                Debug.Log("ray"); 
                Gizmos.color = Color.blue; 
                Target target = hit.transform.GetComponent<Target>();
                Vector3 dir = transform.TransformDirection(Vector3.forward) * rangeOfWeapon;
                Gizmos.DrawRay(shootSpot.position, dir); 
                if(target != null)
                {
                    target.TakeDamage(damage); 
                }
            }
        }
    }
}
