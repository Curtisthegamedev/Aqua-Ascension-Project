/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using Photon.Pun;
using System.IO; 
using Photon.Realtime; 

public class Room : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static Room room;
    private PhotonView pv;

    public bool isGameLoaded;
    public int Scene;

    private Player[] OnlinePlayers;
    private int playersInRoomAmount;
    public int playersInGameAmount; 
    public int myNumInRoom; 
    private void Awake()
    {
        if(Room.room == null)
        {
            Room.room = this; 
        }
        else
        {
            if(Room.room != this)
            {
                Destroy(Room.room.gameObject);
                Room.room = this; 
            }
        }
        DontDestroyOnLoad(this.gameObject); 
    }

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        OnlinePlayers = PhotonNetwork.PlayerList;
        playersInRoomAmount = OnlinePlayers.Length;
        myNumInRoom = playersInRoomAmount; 
    }

    private void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading(); 
    }

    private void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading(); 
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        
    }

    [PunRPC]
    private void LoadedGameScene()
    {
        if(playersInGameAmount == PhotonNetwork.PlayerList.Length)
        {
            pv.RPC("RPC_CreatePlayer", RpcTarget.All); 
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerPrefab"), transform.position, Quaternion.identity, 0); 
    }


}
*/