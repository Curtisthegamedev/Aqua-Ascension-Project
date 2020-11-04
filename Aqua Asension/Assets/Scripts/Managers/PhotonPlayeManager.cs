using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PhotonPlayeManager : MonoBehaviour
{
    [SerializeField] GameObject PrefabPlayer;
    private void Start()
    {
        PhotonNetwork.Instantiate(PrefabPlayer.name, new Vector3(70, -2, -39), Quaternion.identity); 
    }
}
