using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class GenerateLobbyCode : MonoBehaviour
{
    private string lobbyCode;
    private int lobbyCodeNumber;
    [SerializeField] TextMeshProUGUI textDisplay; 

    public void GenerateRandomLobbyCode()
    {
        lobbyCodeNumber = Random.Range(1000, 9999);
        lobbyCodeNumber.ToString(); 
        textDisplay.SetText("Number: " + lobbyCodeNumber); 
    }

    private void Start()
    {
        GenerateRandomLobbyCode(); 
    }
}
