using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class OnlineNameHandler : MonoBehaviour
{
    [SerializeField] Button JoinButton;
    [SerializeField] Button CreateLobbyButton;
    [SerializeField] TMP_InputField PlayerOnlineNickname;
    private bool PlayerNameSet = false; 

    private void Update()
    {
        JoinButton.interactable = !string.IsNullOrEmpty(PlayerOnlineNickname.text) && PlayerNameSet;
        CreateLobbyButton.interactable = !string.IsNullOrEmpty(PlayerOnlineNickname.text) && PlayerNameSet;
    }

    public void SetPlayerName()
    {
        PhotonNetwork.NickName = PlayerOnlineNickname.text;
        PlayerNameSet = true; 
    }
}
