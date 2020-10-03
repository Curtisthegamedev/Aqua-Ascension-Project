using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using Photon.Pun; 

public class LobbyBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PlayerOneName;

    private void Start()
    {
        if(PhotonNetwork.NickName != "" && PlayerOneName.text != "Player Name")
        {
            PlayerOneName.text = PhotonNetwork.NickName;
        }
        else
        {
            PlayerOneName.text = "Error"; 
        }
        
    }
}
