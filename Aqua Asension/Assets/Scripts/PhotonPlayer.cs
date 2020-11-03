using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO; 

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawner = Random.Range(0, Spawning.SpawnScript.spawnpoints.Length); 
        if(PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerPrefab"), 
                Spawning.SpawnScript.spawnpoints[spawner].position, 
                Spawning.SpawnScript.spawnpoints[spawner].rotation, 0); 
        }
    }
}
