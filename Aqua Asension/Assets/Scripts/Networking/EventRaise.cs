using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class EventRaise : MonoBehaviourPun
{
    [SerializeField] GameObject PlayerGraphic;
    private Material playersMaterial; 

    private void Awake()
    {
        playersMaterial = PlayerGraphic.GetComponent<Material>();
    }

    private void Update()
    {
        if(base.photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAllPlayerLooks(); 
        }
    }

    private void ChangeAllPlayerLooks()
    {
        playersMaterial.color = Color.red;

        object[] datas = new object[] { playersMaterial.color }; 
        //PhotonNetwork.RaiseEvent()
    }
}
