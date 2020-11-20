using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class EventRaise : MonoBehaviourPun
{
    [SerializeField] GameObject PlayerGraphic;
    private Material playersMaterial;

    private const byte Player_Change_Look_Event = 0; 
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

        object[] datas = new object[] {base.photonView.ViewID, playersMaterial.color };
        PhotonNetwork.RaiseEvent(Player_Change_Look_Event, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);

    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventRecieved; 
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventRecieved;
    }

    private void NetworkingClient_EventRecieved(EventData obj)
    {
        if(obj.Code == Player_Change_Look_Event)
        {
            object[] datas = (object[])obj.CustomData;
            Material newMatColour = (Material)datas[0];
            //this.gameObject.GetComponent<Material>().color = newMatColour; 
        }
    }
}
