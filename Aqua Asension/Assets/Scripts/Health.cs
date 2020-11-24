using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI; 

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    public int myHealth = 100;
    [SerializeField] Image healthBar; 

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(myHealth); 
        }
        else
        {
            myHealth = (int)stream.ReceiveNext(); 
        }
    }

   
    public void DamageHealth(int damage)
    {
        myHealth -= damage;
        Debug.Log("PlayerHealth is:" + myHealth);
        healthBar.fillAmount = myHealth; 
        if(myHealth <= 0)
        {
            
        }
    }

    
}
