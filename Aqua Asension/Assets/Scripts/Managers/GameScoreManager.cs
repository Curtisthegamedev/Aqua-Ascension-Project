/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameScoreManager : MonoBehaviour
{
    public static GameScoreManager instance;
    public int MyScore = 0;
    public int EnemyOneScore = 0;
    public int EnemyTwoScore = 0;
    public int EnemyThreeScore = 0;
    private int EnemyHighestScore = 0;

    //check works with text mesh pro
    [SerializeField] TextMeshProUGUI MyScoreText;
    [SerializeField] TextMeshProUGUI EnemyHighestScoreText;

    private void Awake()
    {
        instance = this;
        UpdateScoreText();
    }

    private void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        MyScoreText.text = MyScore.ToString();
        EnemyHighestScoreText.text = EnemyHighestScore.ToString();
    }
}*/
