using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DeathMatchTimer : MonoBehaviour
{
    private float timeLeft = 60f;
    private float startTime = 60f;
    [SerializeField] GameObject CoundownText;
    [SerializeField] GameObject GameEndText; 

    private void Start()
    {
        timeLeft = startTime;
    }

    private void Update()
    {
        timeLeft -= 1 * Time.deltaTime;
        CoundownText.GetComponent<TextMeshProUGUI>().text = timeLeft.ToString("0"); 

        if(timeLeft < 0)
        {
            GameEndText.SetActive(true); 
        }
    }
}
