using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class Launch : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject CanvasMainMenu; 
    private void Start()
    {
        Debug.Log("Attempting to connect to master..."); 
        PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Master"); 
        PhotonNetwork.JoinLobby(); 
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
        CanvasMainMenu.SetActive(true); 
    }
}
