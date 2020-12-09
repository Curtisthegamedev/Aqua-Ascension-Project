using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.Events; 

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    private int maxHealth = 100;
    //[SerializeField] Image healthBar;
    [SerializeField] Transform bar;

    //Score Manage
    public int MyScore = 0;
    public int EnemyOneScore = 0;
    public int EnemyTwoScore = 0;
    public int EnemyThreeScore = 0;
    private int EnemyHighestScore = 0;
    [SerializeField] TextMeshProUGUI MyScoreText;
    [SerializeField] TextMeshProUGUI EnemyHighestScoreText;

    List<Transform> spawnPoints = new List<Transform>();
    int health;
    float offset;

    private void Awake()
    {
        
    }
    public void Start()
    {
        health = maxHealth; 
        ResetHealth();
        MyScoreText = GameObject.FindGameObjectWithTag("MyScore").GetComponent<TextMeshProUGUI>();
        EnemyHighestScoreText = GameObject.FindGameObjectWithTag("EnemyScore").GetComponent<TextMeshProUGUI>();
        Debug.Log("my score text: " + MyScoreText); 
        Transform container = GameObject.FindGameObjectWithTag("SpawnContainer").transform;
        foreach(Transform spawn in container)
            spawnPoints.Add(spawn);
        Debug.Log("starting health: " + health); 
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(health); 
        }
        else
        {
            health = (int)stream.ReceiveNext(); 
        }
    }
   
    public void DamageHealth(int damage)
    {
        health -= damage;
        Debug.Log("PlayerHealth is:" + health);
        bar.localScale = new Vector3((float)health/100, 1f); 
        if (health <= 0)
        {
            if(photonView.IsMine)
            {
                //TODO: Update GameManager...
                Respawn();
                bar.localScale = new Vector3((float)health / 100, 1f);
                photonView.RPC("RPC_OnPlayerDeath", RpcTarget.All);
                photonView.RPC("RPC_OnPlayerDeathChangeScoreEnemy", RpcTarget.Others);
            }
            
        }
        UpdateMyScoreText(); 
    }

    [PunRPC]
    void RPC_OnPlayerDeath()
    {
        //if(photonView.IsMine)
        //{
            EnemyHighestScore += 1;  
        //}
    }

    [PunRPC]
    void RPC_OnPlayerDeathChangeScoreEnemy()
    {
        MyScore += 1;
        UpdateEnemyScoreText(); 
    }

    private void UpdateMyScoreText()
    {
        //MyScoreText.text = MyScore.ToString();
        EnemyHighestScoreText.text = EnemyHighestScore.ToString();
    }

    private void UpdateEnemyScoreText()
    {
        MyScoreText.text = MyScore.ToString(); 
    }

    public void ResetHealth() => health = maxHealth; 
    
    public void Respawn()
    {
        GetComponent<CharacterController>().enabled = false; 
        var spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        var position = spawn.position;
        transform.position = position;
        GetComponent<CharacterController>().enabled = true; 
        ResetHealth();
    }
}
