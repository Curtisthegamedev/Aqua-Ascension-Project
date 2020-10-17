using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AquaAscension.Utils;

// This class creates rooms by procedurally linking components together.
public struct Container
{
    public dynamic x, y, w, h, padding;
    public (dynamic, dynamic) Size { get => (w, h); set => (w, h) = value; }
    public (dynamic, dynamic) Position { get => (x, y); set => (x, y) = value; }
    public dynamic Area { get => w * h; }
    public dynamic TotalArea { get => (w + padding) * (h + padding); }
}

[System.Serializable]
public class QuadTree
{
    [SerializeField] RectInt boundary;
    [SerializeField] QuadTree a, b, c, d;
    //  [A][B]
    //  [C][D]
    LinkedList<QuadTree> tree;

    public RectInt Boundary { get => boundary; }

    public QuadTree(RectInt boundary)
    {
        this.boundary = boundary;
        tree = new LinkedList<QuadTree>();
    }

    public void DrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(boundary.x, boundary.y), new Vector3(boundary.width * 2, boundary.height * 2));
            a?.DrawGizmos(); b?.DrawGizmos(); c?.DrawGizmos(); d?.DrawGizmos();
    }

    public void Generate(int iterations, int totalIterations = -1, LinkedList<QuadTree> localTree = null)
    {
        if (iterations <= 0) return;
        
        if(totalIterations <= 0)
        {
            totalIterations = iterations;
            localTree = new LinkedList<QuadTree>();
            localTree.AddFirst(new LinkedListNode<QuadTree>(this));
        }

        var node = localTree.Find(this); 
        
        if (Rand.Chance(1f - 1f / iterations / totalIterations))
        {
            iterations--;
            
            Subdivide();
            
            localTree.AddAfter(node, a);
            localTree.AddAfter(node, b);
            localTree.AddAfter(node, c);
            localTree.AddAfter(node, d);

            a.Generate(iterations, totalIterations, localTree);
            b.Generate(iterations, totalIterations, localTree);
            c.Generate(iterations, totalIterations, localTree);
            d.Generate(iterations, totalIterations, localTree);
        }
    }

    private void Subdivide()
    {
        a = new QuadTree(new RectInt(boundary.xMin - boundary.width / 2, boundary.yMin - boundary.height / 2, boundary.width / 2, boundary.height / 2));
        b = new QuadTree(new RectInt(boundary.xMin + boundary.width / 2, boundary.yMin - boundary.height / 2, boundary.width / 2, boundary.height / 2));
        c = new QuadTree(new RectInt(boundary.xMin - boundary.width / 2, boundary.yMin + boundary.height / 2, boundary.width / 2, boundary.height / 2));
        d = new QuadTree(new RectInt(boundary.xMin + boundary.width / 2, boundary.yMin + boundary.height / 2, boundary.width / 2, boundary.height / 2));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RoomGenerator), true)]
public class RoomGeneratorEditor : Editor
{
    RoomGenerator roomGen;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(12);

        if (GUILayout.Button("Generate"))
            roomGen.OnRegenerate();
    }

    void OnEnable() => roomGen = target as RoomGenerator;
}
#endif

[ExecuteAlways]
public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private RectInt room = new RectInt(0, 0, 100, 100);
    [SerializeField] private QuadTree qt;
    [SerializeField] private int iterations = 4;

    private void Start() => Generate();

    private void Generate()
    {
        //TODO Get the Room size...
        qt = new QuadTree(room);
        qt.Generate(iterations);
    }

    public void OnRegenerate() => Generate();

    private void OnDrawGizmos() => qt.DrawGizmos();
}
