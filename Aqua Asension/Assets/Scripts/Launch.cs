using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launch : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject CanvasMainMenu;
    [SerializeField] GameObject CanvasLoad;
    [SerializeField] GameObject GameNumbers;

    private void Start()
    {
        Debug.Log("Attempting to connect to master...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
        CanvasLoad.SetActive(false);
        CanvasMainMenu.SetActive(true);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(GameNumbers.GetComponentInParent<TextMeshProUGUI>().text);
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
}