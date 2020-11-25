using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Events; 

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    public int myHealth = 100;
    [SerializeField] Image healthBar;
    private Transform SpawnContainer;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    UnityEvent <GameObject>respawnEvent; 

    public void Initialize(UnityEvent <GameObject>RespawnEvent)
    {
        respawnEvent = RespawnEvent;
    }

    private void Start()
    {
        SpawnContainer = GameObject.FindGameObjectWithTag("SpawnContainer").transform; 
        foreach (Transform child in SpawnContainer)
            spawnPoints.Add(child); 
    }

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
            respawnEvent?.Invoke(this.gameObject);
            Debug.Assert(respawnEvent != null); 
        }
    }

    public void ResetHealth()
    {
        myHealth = 100; 
    }

    
}
