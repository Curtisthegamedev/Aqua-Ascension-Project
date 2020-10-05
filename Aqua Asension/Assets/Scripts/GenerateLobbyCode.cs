using UnityEngine;
using TMPro; 

public class GenerateLobbyCode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDisplay; 
    
    private string lobbyCode;
    private int lobbyCodeNumber;

    private void Start() => GenerateRandomLobbyCode(); 
    
    public void GenerateRandomLobbyCode()
    {

    }
}
