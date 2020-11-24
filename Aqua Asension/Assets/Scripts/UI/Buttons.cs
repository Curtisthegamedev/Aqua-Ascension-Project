using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Buttons : MonoBehaviour
{
    GameObject startGameButton;
    private void Start()
    {
        startGameButton = GameObject.FindGameObjectWithTag("StartGameButton");
    }
    
    public void OnClick_StartGame()
    {
        Debug.Log(startGameButton.name);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
        else
        {
            Debug.Log("not clicked by mater Cliet");
        }
    }
}
