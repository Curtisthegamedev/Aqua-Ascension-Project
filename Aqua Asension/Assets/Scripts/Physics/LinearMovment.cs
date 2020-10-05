using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovment : MonoBehaviour
{
    public float speed;
    public float start;
   



    // Start is called before the first frame update
    void Start()
    {


    }



    // Update is called once per frame
    void Update()
    {
        //     transform.position += new Vector3(start, 0, 0).normalized * speed * Time.deltaTime;

        transform.position += new Vector3((start ), 0, 0).normalized * speed * Time.deltaTime;



    }
}
