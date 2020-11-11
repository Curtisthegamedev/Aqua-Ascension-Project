using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject TempBullet;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("shooting"); 
            Instantiate(TempBullet, FirePoint.position, FirePoint.rotation); 
        }
    }
}
