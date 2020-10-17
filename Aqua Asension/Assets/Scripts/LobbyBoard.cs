using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using Photon.Pun; 

public class LobbyBoard : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI PlayerOneName;
    //[SerializeField] TextMeshProUGUI PlayerTwoName;
    //[SerializeField] TextMeshProUGUI PlayerThreeName;
    //[SerializeField] TextMeshProUGUI playerFourName;

    [SerializeField] TextMeshProUGUI[] PlayerNameSlots; 
    private string PlayerNameDefaultText;
    private int timer = 5;
    private bool MoreThanOnePlayerInLobby = false; 

    private enum NameSlots
    {
        One, 
        Two, 
        Three, 
        Four
    }

    private void Start()
    {
        PlayerNameDefaultText = "Player Name";

        for(int i = 0; i <= 4; i++)
        {
            if(PlayerNameSlots[i].text == PlayerNameDefaultText)
            {
                PlayerNameSlots[i].text = PhotonNetwork.NickName;
                i = 5; 
            }
        }
    }
}
