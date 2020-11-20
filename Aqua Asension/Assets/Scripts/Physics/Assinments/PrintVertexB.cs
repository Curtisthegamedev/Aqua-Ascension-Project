using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintVertexB : MonoBehaviour
{
    Mesh mesh;
    private Vector3 vertexPos1B;
    private Vector3 vertexPos2B;
    private Vector3 vertexPos3B;
    private Vector3 vertexPos4B;

    public Vector3 vertexPosB1WS;
    public Vector3 vertexPosB2WS;
    public Vector3 vertexPosB3WS;
    public Vector3 vertexPosB4WS;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertexPos1B = mesh.vertices[0];
        vertexPos2B = mesh.vertices[1];
        vertexPos3B = mesh.vertices[2];
        vertexPos4B = mesh.vertices[3]; 

        vertexPosB1WS = transform.TransformPoint(vertexPos1B);
        vertexPosB2WS = transform.TransformPoint(vertexPos2B);
        vertexPosB3WS = transform.TransformPoint(vertexPos3B);
        vertexPosB4WS = transform.TransformPoint(vertexPos4B);


        Debug.Log("VertexB One is " + vertexPosB1WS);
            Debug.Log("vertexB Two is " + vertexPosB2WS);
            Debug.Log("VertexB three is " + vertexPosB3WS);
            Debug.Log("VertexB four is " + vertexPosB4WS);
    }
}
