using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createtetrahedron : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verts;
    int[] triangles;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; 


    }

    private void MakeTetrahedron()
    {
        verts = new Vector3[]
        {
            new Vector3 (0, 0, 0),
            new Vector3 (0, 0, 1), 
            new Vector3 (1, 0, 0)
        };

        triangles = new int[]
        {
            0, 1, 2
        };
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verts;
        mesh.triangles = triangles; 
    }
}
