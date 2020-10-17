﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

#if UNITY_EDITOR
[CustomEditor(typeof(Launch))]
public class LaunchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        if(GUILayout.Button("Reset Player Prefs"))
            PlayerPrefs.DeleteKey("Gamesettings_Nickname");
    }
}
#endif

public class Launch : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject gameNumbers;
    [SerializeField] GameObject canvasLoad;
    [SerializeField] GameObject canvasMainMenu;
    [SerializeField] GameObject canvasCreateLobby;
    [SerializeField] GameObject canvasJoinLobby;

    [SerializeField] RectTransform enterNamePanel;
    [SerializeField] RectTransform playerListPanel;
    [SerializeField] RectTransform playerListContainer;

    [Header("Prefabs")]
    [SerializeField] RectTransform playerListPrefab;

    List<string> playerNicknames = new List<string>();

    public string nickname { get; set; } 

    private void Start()
    {
        playerListContainer.gameObject.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("photon Scene Sync is on"); 
        nickname = PlayerPrefs.GetString("Gamesettings_Nickname", "");
        if(!string.IsNullOrEmpty(nickname))
            ConnectToServer(nickname);
        else 
            enterNamePanel.gameObject.SetActive(true);
    }

    private void ConnectToServer(string nickname)
    {
        Debug.Log("Attempting to connect to server...");
        PhotonNetwork.NickName = nickname;
        PhotonNetwork.GameVersion = GameManager.Active.Settings.Version;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void CreateRoom()
    {
        if(!PhotonNetwork.IsConnected) return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 3; // TODO : Change this to a higher player count;
        PhotonNetwork.CreateRoom(null, options, null);
    }

    private void OnPlayerlistChanged()
    {
        ClearPlayerList();
        for(var i = 0; i < playerNicknames.Count; i++)
        {
            var playerList = Instantiate(playerListPrefab, playerListPanel.transform);
            playerList.anchoredPosition = new Vector2(0, (-50 * i) - 25);
            playerList.GetComponentInChildren<TextMeshProUGUI>().text = playerNicknames[i];
        }
    }

    private void ClearPlayerList()
    {
        foreach(Transform child in playerListPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    ///////////////////////////////////
    /// PUN Callbacks
    /////////////////////////////////

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server.");
        Debug.Log($"Player Connected: {PhotonNetwork.LocalPlayer.NickName}");
        
        //Connect To Random Room
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server: " + cause.ToString());
        PlayerPrefs.Save();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to lobby.");
        canvasLoad.SetActive(false);
        canvasMainMenu.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        playerListContainer.gameObject.SetActive(true);

        Debug.Log("Connected to room.");

        var names = PhotonNetwork.CurrentRoom.Players.Select(elem => elem.Value.NickName).ToArray();
        Debug.Log("Number of Players Connected: " + names.Length);

        foreach(var name in names)
        {
            Debug.Log(name);
            playerNicknames.Add(name);
        }

        OnPlayerlistChanged();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed To create room: " + message, this.gameObject);
        
        // TODO: Present Error To User...
        Application.Quit();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Failed to join random room: " + message);
        CreateRoom();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player player)
    {
        Debug.Log("Player Entered Room...");
        playerNicknames.Add(player.NickName);
        OnPlayerlistChanged();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player player)
    {
        Debug.Log("Player Left Room...");
        playerNicknames.Remove(player.NickName);
        OnPlayerlistChanged();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("On Room List Updated...");
        // foreach(RoomInfo info in roomList)
        // {

        // }
    }

    ///////////////////////////////////
    /// Actions
    /////////////////////////////////

    public void CreateLobbyAction()
    {
        canvasMainMenu.SetActive(false);
        canvasCreateLobby.SetActive(true);
    }

    public void EnterLobbyAction()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void JoinLobbyAction()
    {
        canvasMainMenu.SetActive(false);
        canvasJoinLobby.SetActive(true); 
    }

    public void JoinAction()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ValidateAndConnectToServerAction()
    {
        if(!IsValidName(nickname))
        {
            Debug.LogWarning("Name is not longer than 4 characters.");
            return;
        }

        string namehash;
        int temp = 0;
        do{
            namehash = nickname + "#" + GenerateHash();
        } while(!IsNameAvailable(namehash) && temp++ < 9999);

        if(temp > 9999)
        {
            Debug.LogWarning("Name + Hash is not available", this.gameObject);
            return;
        }

        PlayerPrefs.SetString("Gamesettings_Nickname", namehash);

        enterNamePanel.gameObject.SetActive(false);
        ConnectToServer(namehash);
    }

    ///////////////////////////////////
    /// Misc
    /////////////////////////////////

    /// <summary>
    /// Generates a String 4 Digit Hash.
    /// </summary>
    protected string GenerateHash()
    {
        return Random.Range(0, 9999).ToString("D4"); 
    }

    // TODO: Check if name is not offensive...or...not...
    protected bool IsValidName(string name)
    {
        return name.Length > 3;
    }

    protected bool IsNameAvailable(string name)
    {
        return true;
    }
}