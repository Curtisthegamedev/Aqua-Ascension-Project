using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class Gun : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject TempBullet;

    private void Update()
    {
        if(photonView.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                
            }
        }
    }

   
}
