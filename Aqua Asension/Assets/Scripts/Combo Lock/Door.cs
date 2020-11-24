using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Switch sw;
    Vector3 pos;
    public static bool unlocked = false;
    private void Start()
    {
        pos = transform.position; 
    }

    private void Update()
    {
        if(sw.Open)
        {
            pos.y += 0.1f;
            transform.position = pos;
        }
    }
}
