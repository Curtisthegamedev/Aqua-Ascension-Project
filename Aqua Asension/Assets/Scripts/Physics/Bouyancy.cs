using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour
{
    private Rigidbody rb;
    private float LiquidDesity = 3000.0f;
    private float volume = 1.0f;
    private Vector3 Boyancy; 

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Boyancy = LiquidDesity * volume; 
    }
}
