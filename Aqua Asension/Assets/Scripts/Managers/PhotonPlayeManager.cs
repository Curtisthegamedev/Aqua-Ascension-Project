using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PhotonPlayeManager : MonoBehaviour
{
    [SerializeField] GameObject PrefabPlayer;
    private bool firstPlayerSpawned = false;
    private bool secondPlayerSpawned = false;
    private bool ThirdPlayerSpawned = false; 
    [SerializeField] Transform spawnLocationOne;
    [SerializeField] Transform spawnLocationTwo;
    [SerializeField] Transform spawnLocationThree;
    [SerializeField] Transform spawnLocationFour; 
    private void Start()
    {
        if(!firstPlayerSpawned && !secondPlayerSpawned && !ThirdPlayerSpawned)
        {
            PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(70, -2, -39), Quaternion.identity);
            firstPlayerSpawned = true; 
        }
        else if(firstPlayerSpawned && !secondPlayerSpawned && !ThirdPlayerSpawned)
        {
            PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(70, -4, -56), Quaternion.identity);
            secondPlayerSpawned = true; 
        }
        else if(firstPlayerSpawned && secondPlayerSpawned && !ThirdPlayerSpawned)
        {
            PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(58, -2, -56), Quaternion.identity);
            ThirdPlayerSpawned = true; 
        }
        else
        {
            PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(57, -2, -38), Quaternion.identity); 
        }
    }
}
