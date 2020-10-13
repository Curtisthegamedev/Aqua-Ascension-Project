using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPoints : MonoBehaviour
{

    [SerializeField] GameObject M1A;
    [SerializeField] GameObject M2A;
    [SerializeField] GameObject M3A;
    [SerializeField] GameObject M4A;
    [SerializeField] GameObject M5A;

    [SerializeField] GameObject M1b;
    [SerializeField] GameObject M2b;
    [SerializeField] GameObject M3b;
    [SerializeField] GameObject M4b;
    [SerializeField] GameObject M5b;

    // Start is called before the first frame update
    void Start()
    {

        print("The points for object 1 are" + M1A.transform.position +"  , " + M2A.transform.position + "  , " + M3A.transform.position + "  , " + M4A.transform.position + "   ,  " + M5A.transform.position);

    }

   
}
