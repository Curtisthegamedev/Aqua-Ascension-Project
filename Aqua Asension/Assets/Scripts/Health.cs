﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Events; 

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] int maxHealth = 100;
    //[SerializeField] Image healthBar;
    [SerializeField] Transform bar; 

    List<Transform> spawnPoints = new List<Transform>();
    int health;
    float offset;

    public void Start()
    {
        ResetHealth();
        

        Transform container = GameObject.FindGameObjectWithTag("SpawnContainer").transform;
        foreach(Transform spawn in container)
            spawnPoints.Add(spawn);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(health); 
        }
        else
        {
            health = (int)stream.ReceiveNext(); 
        }
    }
   
    public void DamageHealth(int damage)
    {
        health -= damage;
        Debug.Log("PlayerHealth is:" + health);
        bar.localScale = new Vector3((float)health/100, 1f); 
        if (health <= 0)
        {
            //TODO: Update GameManager...
            Respawn();
            bar.localScale = new Vector3((float)health / 100, 1f); 
        }
    }

    public void ResetHealth() => health = maxHealth; 
    
    public void Respawn()
    {
        GetComponent<CharacterController>().enabled = false; 
        var spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        var position = spawn.position;
        transform.position = position;
        GetComponent<CharacterController>().enabled = true; 
        ResetHealth();
    }
}
