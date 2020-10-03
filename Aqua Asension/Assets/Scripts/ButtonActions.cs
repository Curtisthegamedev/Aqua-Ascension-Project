using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonActions : MonoBehaviour
{
    //Variables 
    [SerializeField] GameObject canvasMainMenu;
    [SerializeField] GameObject canvasCreateLobby;
    [SerializeField] GameObject canvasJoinLobby;
     
    public void CreateLobby()
    {
        canvasMainMenu.SetActive(false);
        canvasCreateLobby.SetActive(true);
    }

    public void enterLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void JoinLobby()
    {
        canvasMainMenu.SetActive(false);
        canvasJoinLobby.SetActive(true); 
    }

    public void Join()
    {
        SceneManager.LoadScene("Lobby");
    }
}
