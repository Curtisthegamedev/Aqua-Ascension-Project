using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovment : MonoBehaviour
{
    //This will reprisent endpointOne for Par 2 and endpointTwo for Par.
    [SerializeField] Transform endpoint;
    [SerializeField] float angularVel = 45.0f;
    private Rigidbody r;
    private float velosity = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        //Movement physics
        transform.position = Vector3.MoveTowards(transform.position, endpoint.transform.position, Time.deltaTime * velosity);

        //Rotation phisics
        transform.Rotate(0, angularVel * Time.deltaTime, 0, Space.Self); 

    }
}
