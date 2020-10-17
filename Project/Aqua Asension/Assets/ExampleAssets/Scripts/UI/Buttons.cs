using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class Buttons : MonoBehaviour
{
    public void OnClick_StartGame()
    {
        PhotonNetwork.LoadLevel(1); 
    }
}
