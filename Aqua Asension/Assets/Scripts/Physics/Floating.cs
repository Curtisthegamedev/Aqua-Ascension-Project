using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject water;
    private float InitialSubmerge = 1.0f;
    private float displacment = 3.0f;



    private void Start()
    {
        water = GameObject.FindGameObjectWithTag("Water");
    }
    private void Update()
    {
        if (transform.position.y < water.transform.position.y)
        {
            Debug.Log("true");
            float multiplyDisplacement = Mathf.Clamp01(water.transform.position.y -
                transform.position.y /
                InitialSubmerge) * displacment;
            rb.AddForce(new Vector3(0.0f, Mathf.Abs(Physics.gravity.y) * multiplyDisplacement, 0.0f), ForceMode.Acceleration);
        }
    }
}