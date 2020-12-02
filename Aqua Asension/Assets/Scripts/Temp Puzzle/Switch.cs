using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool Open = false;
    public static bool unlocked = false;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Bullet" && unlocked)
        {
            Open = true;
        }
    }

    public void Unlocked(bool open)
    {
        unlocked = open;
        Debug.Log("Door.cs unlocked =" + open);
    }
}
