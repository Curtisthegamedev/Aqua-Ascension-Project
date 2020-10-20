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

    protected List<QuadTree> children;
    protected QuadTree parent;

    public RectInt Boundary { get; protected set; }
    public bool IsLeaf { get => children == null || children.Count <= 0; }
    public bool HasParent { get => parent != null; }

    public QuadTree(RectInt boundary, QuadTree parent = null)
    {
        this.children = new List<QuadTree>();
        this.Boundary = boundary;
        this.parent = parent;
    }

    public QuadTree GetEqualOrGreaterNeighbour(Direction direction = Direction.N) => GetEqualOrGreaterNeighbour(this, direction);
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
    [SerializeField] private int iterations = 4;

    private QuadTree rtNode;
    private QuadTree nNode;

    private void Start() => Generate();

    private void Generate()
    {
        //TODO Get the Room size...
        root = new QuadTree(room);
        root.Generate(iterations);
        quadTrees.Clear();
        quadTrees = QuadTree.LevelTraversal(root);
        rtNode = quadTrees[50];
        nNode = rtNode.GetEqualOrGreaterNeighbour();
        //rtNode = root.GetNode(new Queue<QuadTree.Quadrant>(new[] { QuadTree.Quadrant.NE, QuadTree.Quadrant.SE, QuadTree.Quadrant.SW }));
    }

    public void OnRegenerate() => Generate();

    private void OnDrawGizmos()
    {
        root.DrawGizmos();
        Gizmos.color = Color.green;
        rtNode?.DrawGizmos();
        nNode?.DrawGizmos();
    }
}
