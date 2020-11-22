using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun; 
using Photon.Realtime;

public class PhotonPlayerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject PrefabPlayer;
    [SerializeField] Transform spawnPointsContainer;
    [SerializeField] float offset = 1f;
    
    [Header("Debug")]
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    bool firstPlayerSpawned = false;
    bool secondPlayerSpawned = false;
    bool ThirdPlayerSpawned = false; 

    Player[] playersCache;

    private void Start()
    {
        #region Delete?
        // if(!firstPlayerSpawned && !secondPlayerSpawned && !ThirdPlayerSpawned)
        // {
        //     Debug.Log("playerspawn"); 
        //     PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(670, 10, -38), Quaternion.identity);
        //     firstPlayerSpawned = true; 
        // }
        // else if(firstPlayerSpawned && !secondPlayerSpawned && !ThirdPlayerSpawned)
        // {
        //     PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(675, 10, -56), Quaternion.identity);
        //     secondPlayerSpawned = true; 
        // }
        // else if(firstPlayerSpawned && secondPlayerSpawned && !ThirdPlayerSpawned)
        // {
        //     PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(680, 10, -56), Quaternion.identity);
        //     ThirdPlayerSpawned = true; 
        // }
        // else
        // {
        //     PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(665, 10, -38), Quaternion.identity); 
        // }
        #endregion

        Debug.Assert(PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom);

        playersCache = PhotonNetwork.PlayerList;

        foreach(Transform child in spawnPointsContainer)
            spawnPoints.Add(child);

        for(int i = playersCache.Length - 1; i >= 0; --i)
        {
            Spawn(playersCache[i].NickName);
        }
    }

    protected void Spawn(string NickName)
    {
        Debug.Assert(spawnPoints.Count > 0, this); // Need to have atleast one spawn point

        var spawn = spawnPoints[0];
        var position = spawn.position + new Vector3(0, offset, 0);
        
        GameObject player = PhotonNetwork.Instantiate(PrefabPlayer.name, position, Quaternion.identity);
        player.name = NickName;

        spawnPoints.Add(spawn);
        spawnPoints.RemoveAt(0);
    }
}
