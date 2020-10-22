using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintVertexPositions : MonoBehaviour
{
    Mesh mesh;
    private Vector3 vertexPos1;
    private Vector3 vertexPos2;
    private Vector3 vertexPos3;
    //Vector3 vertexPos4;
    private Vector3 vertexPos4;
    //Vector3 vertexPos6;
    //Vector3 vertexPos7;
    //Vector3 vertexPos8;
    private Vector3 vertexPos5;

    public Vector3 vertexPos1WS;
    public Vector3 vertexPos2WS;
    public Vector3 vertexPos3WS;
    public Vector3 vertexPos4WS;
    public Vector3 vertexPos5WS; 

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertexPos1 = mesh.vertices[0];
        vertexPos2 = mesh.vertices[1];
        vertexPos3 = mesh.vertices[2];
        //vertexPos4 = mesh.vertices[3];
        vertexPos4 = mesh.vertices[4];
        //vertexPos6 = mesh.vertices[5];
        //vertexPos7 = mesh.vertices[6];
        //vertexPos8 = mesh.vertices[7];
        vertexPos5 = mesh.vertices[8];

        vertexPos1WS = transform.TransformPoint(vertexPos1);
        vertexPos2WS = transform.TransformPoint(vertexPos2);
        vertexPos3WS = transform.TransformPoint(vertexPos3);
        vertexPos4WS = transform.TransformPoint(vertexPos4);
        vertexPos5WS = transform.TransformPoint(vertexPos5); 

        
            Debug.Log("Vertex One is " + vertexPos1WS);
            Debug.Log("Vertex Two is " + vertexPos2WS);
            Debug.Log("vertex Three is " + vertexPos3WS);
            Debug.Log("vertex four is " + vertexPos4WS);
            Debug.Log("vertex five is " + vertexPos5WS);
        
        
    }
}
