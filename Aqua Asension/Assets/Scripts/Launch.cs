using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class Launch : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject gameNumbers;
    [SerializeField] GameObject canvasLoad;
    [SerializeField] GameObject canvasMainMenu;
    [SerializeField] GameObject canvasCreateLobby;
    [SerializeField] GameObject canvasJoinLobby;

    private void Start()
    {
        Debug.Log("Attempting to connect to server...");
        PhotonNetwork.GameVersion = GameManager.Active.Settings.Version;
        PhotonNetwork.NickName = GameManager.Active.Settings.NickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to server.");
        Debug.Log($"Player Connected: {PhotonNetwork.LocalPlayer.NickName}");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server: " + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
        canvasLoad.SetActive(false);
        canvasMainMenu.SetActive(true);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(gameNumbers.GetComponentInParent<TextMeshProUGUI>().text);
    }

    public override void OnJoinedRoom()
    {

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
    }

    /// Actions

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

    //  Misc

    /// <summary>
    /// NOTE: ONLY CALL THIS ONCE!
    /// </summary>
    /// <returns>Generated a String 4 Digit Hash</returns>
    protected string GenerateHash()
    {
        return Random.Range(0, 9999).ToString("D4"); 
    }
}