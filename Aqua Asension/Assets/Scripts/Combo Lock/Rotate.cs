using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotate : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };

    private bool coroutineAllowed;

    private int numberShown;

    private void Start()
    {
        coroutineAllowed = true;
        numberShown = 0;
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire2") && coroutineAllowed)
        {
            StartCoroutine("RotateDown");
        }
        if (Input.GetButtonDown("Fire3") && coroutineAllowed)
        {
            StartCoroutine("RotateUp");
        }
    }

    private IEnumerator RotateDown()
    {
        coroutineAllowed = false;

        for (int i = 0; i <= 11; i++)
        {
            transform.Rotate(0f, 0f, 3f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        numberShown += 1;

        if (numberShown > 9)
        {
            numberShown = 0;
        }

        Debug.Log(numberShown);

        Rotated(name, numberShown);
    }

    private IEnumerator RotateUp()
    {
        coroutineAllowed = false;

        for (int i = 0; i <= 11; i++)
        {
            transform.Rotate(0f, 0f, -3f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        numberShown -= 1;

        if (numberShown < 0)
        {
            numberShown = 9;
        }

        Debug.Log(numberShown);

        Rotated(name, numberShown);
    }
}
