using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public enum Quadrant : int
    {
        NW, NE, SW, SE, None = -1
    }

    public enum Direction : int
    {
        N, S, E, W
    }

    public static QuadTree GetEqualOrGreaterNeighbour(QuadTree self, Direction direction = Direction.N)
    {
        if (self == null || self.parent == null) return null;

        // North
        if (direction == Direction.N)
        {
            if (self.parent.children[(int)Quadrant.SW] == self)
                return self.parent.children[(int)Quadrant.NW];
            if (self.parent.children[(int)Quadrant.SE] == self)
                return self.parent.children[(int)Quadrant.NE];

            var node = GetEqualOrGreaterNeighbour(self.parent, direction);
            if (node == null || node.IsLeaf) return node;

            if (self.parent.children[(int)Quadrant.NW] == self)
                return node.children[(int)Quadrant.SW];
            else
                return node.children[(int)Quadrant.SE];
        }
        // South
        else if (direction == Direction.S)
        {
            if (self.parent.children[(int)Quadrant.NW] == self)
                return self.parent.children[(int)Quadrant.SW];
            if (self.parent.children[(int)Quadrant.NE] == self)
                return self.parent.children[(int)Quadrant.SE];

            var node = GetEqualOrGreaterNeighbour(self.parent, direction);
            if (node == null || node.IsLeaf) return node;

            if (self.parent.children[(int)Quadrant.SW] == self)
                return node.children[(int)Quadrant.NW];
            else
                return node.children[(int)Quadrant.NE];
        }
        // East
        else if (direction == Direction.E)
        {
            if (self.parent.children[(int)Quadrant.NW] == self)
                return self.parent.children[(int)Quadrant.NE];
            if (self.parent.children[(int)Quadrant.SW] == self)
                return self.parent.children[(int)Quadrant.SE];

            var node = GetEqualOrGreaterNeighbour(self.parent, direction);
            if (node == null || node.IsLeaf) return node;

            if (self.parent.children[(int)Quadrant.NE] == self)
                return node.children[(int)Quadrant.NW];
            else
                return node.children[(int)Quadrant.SW];
        }
        // West
        else
        {
            if (self.parent.children[(int)Quadrant.NE] == self)
                return self.parent.children[(int)Quadrant.NW];
            if (self.parent.children[(int)Quadrant.SE] == self)
                return self.parent.children[(int)Quadrant.SW];

            var node = GetEqualOrGreaterNeighbour(self.parent, direction);
            if (node == null || node.IsLeaf) return node;

            if (self.parent.children[(int)Quadrant.NW] == self)
                return node.children[(int)Quadrant.NE];
            else
                return node.children[(int)Quadrant.SE];
        }
    }

    public static QuadTree GetEqualNeighbour(QuadTree self, Direction direction = Direction.N)
    {
        var neighbour = GetEqualOrGreaterNeighbour(self, direction);
        if (neighbour == null || !self.HasSameArea(neighbour)) return null;
        return neighbour;
    }

    public static List<QuadTree> LevelTraversal(QuadTree node, int level = int.MaxValue)
    {
        if (node == null) return null;

        if (level <= 1 || node.IsLeaf) return new List<QuadTree>(new[] { node });

        List<QuadTree> quadTreeList = new List<QuadTree>();

        if (node.parent == null) quadTreeList.Add(node);

        foreach (var child in node.children)
            quadTreeList.AddRange(LevelTraversal(child, level - 1));

        return quadTreeList;
    }

    public static bool HasSameArea(QuadTree a, QuadTree b) => a.Area - 1 <= b.Area && a.Area + 1 >= b.Area;

    protected List<QuadTree> children;
    protected QuadTree parent;

    public RectInt Boundary { get; protected set; }
    public int Area { get => Boundary.width * Boundary.height; }
    public bool IsLeaf { get => children == null || children.Count <= 0; }
    public bool HasParent { get => parent != null; }

    public QuadTree(RectInt boundary, QuadTree parent = null)
    {
        this.children = new List<QuadTree>();
        this.Boundary = boundary;
        this.parent = parent;
    }

    public bool HasSameArea(QuadTree other) => HasSameArea(this, other);
    public QuadTree GetEqualOrGreaterNeighbour(Direction direction = Direction.N) => GetEqualOrGreaterNeighbour(this, direction);
    public QuadTree GetEqualNeighbour(Direction direction = Direction.N) => GetEqualNeighbour(this, direction);
    public List<QuadTree> LevelTraversal(int level = int.MaxValue) => LevelTraversal(this, level);

    public void DrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(Boundary.x, Boundary.y), new Vector3(Boundary.width * 2, Boundary.height * 2));
        if (!IsLeaf) children.ForEach(child => child.DrawGizmos());
    }

    public void Generate(int iterations, int totalIterations = -1)
    {
        if (iterations <= 0) return;

        if (totalIterations <= 0) totalIterations = iterations;

        if (Rand.Chance(1f - 1f / iterations / totalIterations))
        {
            iterations--;
            Subdivide();
            children.ForEach(child => child.Generate(iterations, totalIterations));
        }
    }

    protected void Subdivide()
    {
        children.Add(new QuadTree(new RectInt(Boundary.xMin - Boundary.width / 2, Boundary.yMin - Boundary.height / 2, Boundary.width / 2, Boundary.height / 2), this));
        children.Add(new QuadTree(new RectInt(Boundary.xMin + Boundary.width / 2, Boundary.yMin - Boundary.height / 2, Boundary.width / 2, Boundary.height / 2), this));
        children.Add(new QuadTree(new RectInt(Boundary.xMin - Boundary.width / 2, Boundary.yMin + Boundary.height / 2, Boundary.width / 2, Boundary.height / 2), this));
        children.Add(new QuadTree(new RectInt(Boundary.xMin + Boundary.width / 2, Boundary.yMin + Boundary.height / 2, Boundary.width / 2, Boundary.height / 2), this));
    }

    public void Flatten() => children.Clear();

    /// <summary>
    /// This function returns a rect pair  
    /// </summary>
    public (RectInt, RectInt) MergeNode(Direction direction)
    {
        var neighbour = GetEqualOrGreaterNeighbour(this, direction);
        return (this.Boundary, neighbour.Boundary);
    }

    [System.Obsolete("This will give you unexpected values...")]
    public QuadTree GetNode(Queue<Quadrant> quadrants)
    {
        try
        {
            if (IsLeaf || quadrants.Count <= 0) return this;
            var quadrant = quadrants.Dequeue();
            if (quadrant == Quadrant.None) return this;
            return children[(int)quadrant].GetNode(quadrants);
        }
        catch (System.Exception ex) { return this; }
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
    [SerializeField] private QuadTree root;
    [SerializeField] private List<QuadTree> quadTrees;
    [SerializeField] private List<QuadTree> secondPass;
    [SerializeField] private int iterations = 4;

    private void Start() => Generate();

    private void Generate()
    {
        //TODO Get the Room size...
        root = new QuadTree(room);
        root.Generate(iterations);
        quadTrees.Clear();
        quadTrees = root.LevelTraversal();

        Debug.Log($"First pass count - {quadTrees.Count}");

        // Nope, No, Nope. 
        // secondPass = new List<QuadTree>();
        // for (int i = quadTrees.Count - 1; i >= 0; --i)
        // {
        //     if (Rand.Chance(0.4f))
        //     {
        //         int length = Random.Range(1, 4);
        //         var direction = (QuadTree.Direction)Random.Range(0, 5);
        //         var node = quadTrees[Random.Range(0, quadTrees.Count)];
        //         QuadTree[] neighbours = new QuadTree[length];
        //         for (int j = length - 1; j >= 0; --j)
        //             neighbours[j] = node.GetEqualNeighbour(direction);
        //         if (neighbours.All(n => n != null))
        //         {
        //             RectInt newBounds = node.Boundary;
        //             switch (direction)
        //             {
        //                 case QuadTree.Direction.N:
        //                     newBounds.yMin = neighbours[neighbours.Length - 1].Boundary.yMin;
        //                     break;
        //                 case QuadTree.Direction.S:
        //                     newBounds.yMax = neighbours[neighbours.Length - 1].Boundary.yMax;
        //                     break;
        //                 case QuadTree.Direction.W:
        //                     newBounds.xMin = neighbours[neighbours.Length - 1].Boundary.xMin;
        //                     break;
        //                 case QuadTree.Direction.E:
        //                     newBounds.xMax = neighbours[neighbours.Length - 1].Boundary.xMax;
        //                     break;
        //             }
        //             if(root.Boundary.Contains(newBounds.position))
        //                 secondPass.Add(new QuadTree(newBounds));
        //         }
        //     }
        // }
        // Debug.Log($"Second pass count - {secondPass.Count}");
        // //secondPass = secondPass.Distinct().ToList();
        // Debug.Log($"Second pass count after distinction - {secondPass.Count}");
    }

    public void OnRegenerate() => Generate();

    private void OnDrawGizmos()
    {
        root.DrawGizmos();
        for (int i = secondPass.Count - 1; i >= 0; --i)
        {
            Gizmos.color = new Color(0f, 1f, (float)i / secondPass.Count);
            secondPass[i].DrawGizmos();
        }
    }
}
