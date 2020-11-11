using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField] GameObject WinCanvase;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered"); 
        if(other.gameObject.tag == "Player")
        {
            WinCanvase.SetActive(true); 
        }
    }
}
