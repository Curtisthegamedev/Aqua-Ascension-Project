using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    public static Setup setup;

    public Transform[] spawnPoints;

    private void OnEnable()
    {
            if(Setup.setup == null)
        {
            Setup.setup = this; 
        }
    }
}
