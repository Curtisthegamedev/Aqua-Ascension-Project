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
<<<<<<< HEAD
    private string PlayerNameDefaultText;
    private int timer = 5;
    private bool MoreThanOnePlayerInLobby = false; 
=======
    private string PlayerNameDefaultText; 
>>>>>>> 754e70c44158e2b130b387ffb5bc6ea54d2286b6

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
