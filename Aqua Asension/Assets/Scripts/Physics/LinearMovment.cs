using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovment : MonoBehaviour
{
    //This will reprisent endpointOne for Par 2 and endpointTwo for Par.
    [SerializeField] Transform endpoint;
    private float angularVel = 45.0f; 

    private float velosity = 2.5f;


    // Start is called before the first frame update
    void Start()
    {


    }



    // Update is called once per frame
    void Update()
    {
        //Movement physics
        transform.position = Vector3.MoveTowards(transform.position, endpoint.transform.position, Time.deltaTime * velosity); 

        //Rotation phisics


    }
}
