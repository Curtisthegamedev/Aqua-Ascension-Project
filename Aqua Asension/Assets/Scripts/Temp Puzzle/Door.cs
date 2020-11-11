using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Switch sw;
    Vector3 pos;
    float time = 30; 
    private void Start()
    {
        pos = transform.position; 
    }

    private void Update()
    {
        if(sw.WasHit && time > 0)
        {
            pos.y += 0.1f;
            transform.position = pos;
            time -= 0; 
        }
    }
}
