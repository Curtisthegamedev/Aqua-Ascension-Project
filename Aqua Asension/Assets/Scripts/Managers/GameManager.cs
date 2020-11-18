using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts; 

public class GameManager : MonoBehaviour
{
    public static GameManager Active { get; protected set; }

    [SerializeField] private GameSettings settings;

    public GameSettings Settings => settings;

    void Awake() => Active = this;
}
