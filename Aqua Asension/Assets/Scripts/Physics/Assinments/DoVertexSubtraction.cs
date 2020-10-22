using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoVertexSubtraction : MonoBehaviour
{
    //PrintVertexPositions vertexAScirpt;
    //PrintVertexB vertexBScript;

    private GameObject ObjectA;
    private GameObject ObjectB;

    private void Awake()
    {
        ObjectA = GameObject.FindGameObjectWithTag("ParOne");
        ObjectB = GameObject.FindGameObjectWithTag("ParTwo"); 
    }

    private void DoSubtraction()
    {
        //Math for Object A Point 1
        Vector3 resultOne = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS; 
        
        Vector3 resultTwo = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        Vector3 resultThree = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        Vector3 resultFour = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        Vector3 resultFive = ObjectA.GetComponent<PrintVertexPositions>().vertexPos1WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        //Math for Object A point 2
        Vector3 resultOneA2 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        Vector3 resultTwoA2 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        Vector3 resultThreeA2 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        Vector3 resultFourA2 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        Vector3 resultFiveA2 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos2WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        //Math for object A point 3
        Vector3 resultOneA3 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        Vector3 resultTwoA3 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        Vector3 resultThreeA3 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        Vector3 resultFourA3 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        Vector3 resultFiveA3 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos3WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        //Math for object A point 4
        Vector3 resultOneA4 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        Vector3 resultTwoA4 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        Vector3 resultThreeA4 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        Vector3 resultFourA4 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        Vector3 resultFiveA4 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos4WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;


        //Math for object A point 5
        Vector3 resultOneA5 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB1WS;

        Vector3 resultTwoA5 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB2WS;

        Vector3 resultThreeA5 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB3WS;

        Vector3 resultFourA5 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB4WS;

        Vector3 resultFiveA5 = ObjectA.GetComponent<PrintVertexPositions>().vertexPos5WS -
            ObjectB.GetComponent<PrintVertexB>().vertexPosB5WS;

        Debug.Log("resultOneA1 " + resultOne);
        Debug.Log("resultTwoA1 " + resultTwo);
        Debug.Log("resultThreeA1 " + resultThree);
        Debug.Log("resultFourA1 " + resultFour);
        Debug.Log("resultFiveA1 " + resultFive);
        Debug.Log("resultOneA2 " + resultOneA2);
        Debug.Log("resultTwoA2 " + resultTwoA2);
        Debug.Log("resultThreeA2 " + resultThreeA2);
        Debug.Log("resultFourA2 " + resultFourA2);
        Debug.Log("resultFiveA2 " + resultFiveA2);
        Debug.Log("resultOneA3 " + resultOneA3);
        Debug.Log("resultTwoA3 " + resultTwoA3);
        Debug.Log("resultThreeA3 " + resultThreeA3);
        Debug.Log("resultFourA3 " + resultFourA3);
        Debug.Log("resultFiveA3 " + resultFiveA3);
        Debug.Log("resultOneA4 " + resultOneA4);
        Debug.Log("resultTwoA4 " + resultTwoA4);
        Debug.Log("resultThreeA4 " + resultThreeA4);
        Debug.Log("resultFourA4 " + resultFourA4);
        Debug.Log("resultFiveA4 " + resultFiveA4);
        Debug.Log("resultOneA5 " + resultOneA5);
        Debug.Log("resultTwoA5 " + resultTwoA5);
        Debug.Log("resultThreeA5 " + resultThreeA5);
        Debug.Log("resultFourA5 " + resultFourA5);
        Debug.Log("resultFiveA5 " + resultFiveA5); 
        
    }

    private void Start()
    {
        DoSubtraction(); 
    }
}
