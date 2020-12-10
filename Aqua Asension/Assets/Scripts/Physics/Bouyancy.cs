using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouyancy : MonoBehaviour
{
    public static Bouyancy instance;

    private float speed = 1.0f;
    private float amp = 1.0f;
    private float offset = 0.0f;
    private float length = 2.0f;


    private void Awake()
    {
        if(instance = null)
        {
            instance = this; 
        }
        else if(instance != this)
        {
            Destroy(this); 
        }
    }


}
