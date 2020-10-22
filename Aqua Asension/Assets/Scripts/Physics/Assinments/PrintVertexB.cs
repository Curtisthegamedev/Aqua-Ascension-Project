using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintVertexB : MonoBehaviour
{
    Mesh mesh;
    private Vector3 vertexPos1B;
    private Vector3 vertexPos2B;
    private Vector3 vertexPos3B;
    //Vector3 vertexPos4;
    private Vector3 vertexPos4B;
    //Vector3 vertexPos6;
    //Vector3 vertexPos7;
    //Vector3 vertexPos8;
    private Vector3 vertexPos5B;

    public Vector3 vertexPosB1WS;
    public Vector3 vertexPosB2WS;
    public Vector3 vertexPosB3WS;
    public Vector3 vertexPosB4WS;
    public Vector3 vertexPosB5WS;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertexPos1B = mesh.vertices[0];
        vertexPos2B = mesh.vertices[1];
        vertexPos3B = mesh.vertices[2];
        //vertexPos4 = mesh.vertices[3];
        vertexPos4B = mesh.vertices[4];
        //vertexPos6 = mesh.vertices[5];
        //vertexPos7 = mesh.vertices[6];
        //vertexPos8 = mesh.vertices[7];
        vertexPos5B = mesh.vertices[8];

        vertexPosB1WS = transform.TransformPoint(vertexPos1B);
        vertexPosB2WS = transform.TransformPoint(vertexPos2B);
        vertexPosB3WS = transform.TransformPoint(vertexPos3B);
        vertexPosB4WS = transform.TransformPoint(vertexPos4B);
        vertexPosB5WS = transform.TransformPoint(vertexPos5B);


        Debug.Log("VertexB One is " + vertexPosB1WS);
            Debug.Log("vertexB Two is " + vertexPosB2WS);
            Debug.Log("VertexB three is " + vertexPosB3WS);
            Debug.Log("VertexB four is " + vertexPosB4WS);
            Debug.Log("VertexB five is " + vertexPosB5WS);
        

    }
}
