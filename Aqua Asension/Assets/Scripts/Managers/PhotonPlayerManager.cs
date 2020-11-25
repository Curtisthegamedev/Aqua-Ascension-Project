using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events; 

public class PhotonPlayerManager : MonoBehaviour
{
    public class PlayerSpawnInfo
    {
        public Player Player { get; set; }
        public bool Spawned { get; internal set; }

        public PlayerSpawnInfo(Player player)
        {
            Player = player;
        }
    }

    [Header("References")]
    [SerializeField] GameObject PrefabPlayer;
    [SerializeField] Transform spawnPointsContainer;
    [SerializeField] float offset = 1f;

    [Header("Debug")]
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    UnityEvent<GameObject> respawnPlayerEvent; 

    private void Start()
    {
        Debug.Assert(PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom);

        foreach (Transform child in spawnPointsContainer)
            spawnPoints.Add(child);

        if(respawnPlayerEvent == null)
        {
            respawnPlayerEvent = new UnityEvent<GameObject>(); 
        }
        respawnPlayerEvent.AddListener(Respawn); 

        Spawn();
    }

    protected void Spawn()
    {
        Debug.Assert(spawnPoints.Count > 0, this); // Need to have atleast one spawn point

        var spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        var position = spawn.position + new Vector3(0, offset, 0);

        GameObject player = PhotonNetwork.Instantiate(PrefabPlayer.name, position, Quaternion.identity);
        //player.GetComponent<Health>().Initialize(respawnPlayerEvent); 
    }

    protected void Respawn(GameObject player)
    {
        Debug.Assert(spawnPoints.Count > 0, this); // Need to have atleast one spawn point

        var spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        var position = spawn.position + new Vector3(0, offset, 0);

        player.transform.position = position;
        player.GetComponent<Health>().ResetHealth(); 
    }
}
