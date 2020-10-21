using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using Photon.Pun; 

public class PhotonPlayers : MonoBehaviour
{
    private PhotonView playerPhotonView;
    [SerializeField] GameObject PlayerAvatar; 

    private void Start()
    { 
        playerPhotonView = this.gameObject.GetComponent<PhotonView>();
        int RandomSpawn = Random.Range(0, Setup.setup.spawnPoints.Length); 
        if(playerPhotonView.IsMine)
        {
            PlayerAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonNetworkSpawn.prefab"), 
                Setup.setup.spawnPoints[RandomSpawn].position, Setup.setup.spawnPoints[RandomSpawn].rotation, 0); 
        }
    }


}
