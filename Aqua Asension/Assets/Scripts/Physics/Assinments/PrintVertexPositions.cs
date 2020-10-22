using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintVertexPositions : MonoBehaviour
{
    Mesh mesh;
    Vector3 vertexPos1;
    Vector3 vertexPos2;
    Vector3 vertexPos3;
    //Vector3 vertexPos4;
    Vector3 vertexPos5;
    //Vector3 vertexPos6;
    //Vector3 vertexPos7;
    Vector3 vertexPos8;
    Vector3 vertexPos9; 

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertexPos1 = mesh.vertices[0];
        vertexPos2 = mesh.vertices[1];
        vertexPos3 = mesh.vertices[2];
        //vertexPos4 = mesh.vertices[3];
        vertexPos5 = mesh.vertices[4];
        //vertexPos6 = mesh.vertices[5];
        //vertexPos7 = mesh.vertices[6];
        //vertexPos8 = mesh.vertices[7];
        vertexPos9 = mesh.vertices[8]; 

        Debug.Log("Vertex One is " + transform.TransformPoint(vertexPos1));
        Debug.Log("Vertex Two is " + transform.TransformPoint(vertexPos2));
        Debug.Log("vertex Three is " + transform.TransformPoint(vertexPos3));
        Debug.Log("vertex four is " + transform.TransformPoint(vertexPos5));
        Debug.Log("vertex five is " + transform.TransformPoint(vertexPos9));
    }
}
