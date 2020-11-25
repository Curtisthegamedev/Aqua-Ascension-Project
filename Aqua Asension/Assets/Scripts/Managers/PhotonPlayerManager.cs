using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonPlayerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject PrefabPlayer;
    [SerializeField] Transform spawnPointsContainer;
    [SerializeField] float offset = 1f;

    [Header("Debug")]
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        Debug.Assert(PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom);

        foreach (Transform child in spawnPointsContainer)
            spawnPoints.Add(child);

        Spawn();
    }

    protected void Spawn()
    {
        Debug.Assert(spawnPoints.Count > 0, this); // Need to have atleast one spawn point

        var spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        var position = spawn.position + new Vector3(0, offset, 0);

        GameObject player = PhotonNetwork.Instantiate(PrefabPlayer.name, position, Quaternion.identity);
    }
}
