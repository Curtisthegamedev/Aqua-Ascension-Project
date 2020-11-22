using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using AquaAscension.Utils;

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

    public static bool HasSameArea(QuadTree a, QuadTree b) => a.Area == b.Area;

    protected List<QuadTree> children;
    protected QuadTree parent;

    public Rect Boundary { get; protected set; }
    public float Area { get => Boundary.width * Boundary.height; }
    public bool IsLeaf { get => children.Count <= 0; }
    public bool HasParent { get => parent != null; }

    public QuadTree(Rect boundary, QuadTree parent = null)
    {
        this.children = new List<QuadTree>();
        this.Boundary = boundary;
        this.parent = parent;
    }

    public bool HasSameArea(QuadTree other) => HasSameArea(this, other);
    public QuadTree GetEqualOrGreaterNeighbour(Direction direction = Direction.N) => GetEqualOrGreaterNeighbour(this, direction);
    public QuadTree GetEqualNeighbour(Direction direction = Direction.N) => GetEqualNeighbour(this, direction);
    public List<QuadTree> LevelTraversal(int level = int.MaxValue) => LevelTraversal(this, level);

    //! Fix Wire Drawing, use center, and width, height w/o multiplying by 2...
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
        children.Add(new QuadTree(new Rect(Boundary.xMin - Boundary.width / 2f, Boundary.yMin - Boundary.height / 2f, Boundary.width / 2f, Boundary.height / 2f), this));
        children.Add(new QuadTree(new Rect(Boundary.xMin + Boundary.width / 2f, Boundary.yMin - Boundary.height / 2f, Boundary.width / 2f, Boundary.height / 2f), this));
        children.Add(new QuadTree(new Rect(Boundary.xMin - Boundary.width / 2f, Boundary.yMin + Boundary.height / 2f, Boundary.width / 2f, Boundary.height / 2f), this));
        children.Add(new QuadTree(new Rect(Boundary.xMin + Boundary.width / 2f, Boundary.yMin + Boundary.height / 2f, Boundary.width / 2f, Boundary.height / 2f), this));
    }

    public void Flatten() => children.Clear();

    /// <summary>
    /// This function returns a rect pair  
    /// </summary>
    public (Rect, Rect) MergeNode(Direction direction)
    {
        var neighbour = this.GetEqualNeighbour(direction);
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

[ExecuteInEditMode]
public class RoomGenerator : MonoBehaviour
{
    [System.Serializable]
    public sealed class Section
    {
        public enum Length : int
        {
            L1, L2, L3, L4
        }

        public enum Size : int
        {
            Small, Medium, Large
        }

        public Length length;
        public Size size;
        public List<GameObject> prefabs;
        public float placementProbability;
    }

    public const int SUBDIVIDE_COUNT = 4;

    //? Change This into its own class to define rooms;
    [SerializeField] private RectInt room = new RectInt(0, 0, 100, 100);
    //[SerializeField] private int mergeCount = 4;

    [Header("References")]
    [SerializeField] private List<Section> sectionPrefabs;
    [SerializeField] private Transform roomAnchor;

    [Header("Debug")]
    public bool showFirstPass = true;
    public bool showSecondPass = false;
    public bool showMergePass = false;
    [SerializeField] private QuadTree root;
    [SerializeField] private List<QuadTree> firstPass;
    [SerializeField] private List<QuadTree> secondPass;
    [SerializeField] private List<QuadTree> mergePass;

    private QuadTree selectedNode;
    private QuadTree compareNode;
    private IEnumerator mergeCoroutine;

    private void Start() => Generate();

    private void Generate()
    {
        //TODO Get the Room size...
        root = new QuadTree(new Rect(room.x, room.y, room.width, room.height));
        root.Generate(SUBDIVIDE_COUNT);
        firstPass.Clear();
        firstPass = root.LevelTraversal();

        Debug.Log($"First pass count - {firstPass.Count}");

        #region
        // secondPass = new List<QuadTree>();
        // var u = Rand.Unique(0, quadTrees.Count, 4);
        // for (int i = mergeCount - 1; i >= 0; --i)
        // {
        //     var node = quadTrees[u[i]];
        //     var newBounds = node.Boundary;
        //     var dir = (QuadTree.Direction)Random.Range(0, 4);

        //     //Get a Neighbour in a direction...
        //     var n = node.GetEqualNeighbour();
        //     if (n != null)
        //     {
        //         // switch (dir)
        //         // {
        //         //     case QuadTreeFloat.Direction.N:
        //         //         newBounds.yMin = n.Boundary.yMin;
        //         //         break;
        //         //     case QuadTreeFloat.Direction.S:
        //         //         newBounds.yMax = n.Boundary.yMax;
        //         //         break;
        //         //     case QuadTreeFloat.Direction.W:
        //         //         newBounds.xMin = n.Boundary.xMin;
        //         //         break;
        //         //     case QuadTreeFloat.Direction.E:
        //         //         newBounds.xMax = n.Boundary.xMax;
        //         //         break;
        //         // }   
        //         switch (dir)
        //         {
        //             case QuadTree.Direction.N:
        //                 newBounds.position = new Vector2(newBounds.position.x, newBounds.position.y + n.Boundary.position.y);
        //                 newBounds.size = new Vector2(newBounds.size.x, newBounds.size.y + n.Boundary.size.y);
        //                 break;
        //             case QuadTree.Direction.S:
        //                 newBounds.position = new Vector2(newBounds.position.x, newBounds.position.y - n.Boundary.position.y);
        //                 newBounds.size = new Vector2(newBounds.size.x, newBounds.size.y + n.Boundary.size.y);
        //                 break;
        //             case QuadTree.Direction.W:
        //                 newBounds.position = new Vector2(newBounds.position.x - n.Boundary.position.x, newBounds.position.y);
        //                 newBounds.size = new Vector2(newBounds.size.x + n.Boundary.size.x, newBounds.size.y);
        //                 break;
        //             case QuadTree.Direction.E:
        //                 newBounds.position = new Vector2(newBounds.position.x + n.Boundary.position.x, newBounds.position.y);
        //                 newBounds.size = new Vector2(newBounds.size.x + n.Boundary.size.x, newBounds.size.y);
        //                 break;
        //         }
        //         secondPass.Add(new QuadTree(newBounds));
        //     }

        //     // Get Nearest From N Neighbors in a direction...
        //     // List<QuadTree> nearest = new List<QuadTree>();
        //     // QuadTree next = node;
        //     // var iter = 0;
        //     // while (true)
        //     // {
        //     //     iter++;
        //     //     next = next.GetEqualNeighbour(dir);
        //     //     if (next == null || iter > 999) break;
        //     //     nearest.Add(next);
        //     // }
        //     // if (nearest.Count <= 0) continue;
        //     // var length = Random.Range(0, nearest.Count) - 1;
        //     // length = length < 0 ? 0 : length;
        //     // switch (dir)
        //     // {
        //     //     case QuadTree.Direction.N:
        //     //         newBounds.yMin = nearest[length].Boundary.yMin;
        //     //         break;
        //     //     case QuadTree.Direction.S:
        //     //         newBounds.yMax = nearest[length].Boundary.yMax;
        //     //         break;
        //     //     case QuadTree.Direction.W:
        //     //         newBounds.xMin = nearest[length].Boundary.xMin;
        //     //         break;
        //     //     case QuadTree.Direction.E:
        //     //         newBounds.xMax = nearest[length].Boundary.xMax;
        //     //         break;
        //     // }
        //     // secondPass.Add(new QuadTree(newBounds));
        // }
        // Debug.Log($"Second pass count - {secondPass.Count}");
        // secondPass = secondPass.Distinct().ToList();
        // Debug.Log($"Second pass count after distinction - {secondPass.Count}");
        #endregion

        // Nope, No, Nope. 
        //! SecondPass and merging is broken.
        secondPass = new List<QuadTree>();
        for (int i = firstPass.Count - 1; i >= 0; --i)
        {
            if (Rand.Chance(0.2f))
            {
                int length = Random.Range(1, 5);
                var direction = (QuadTree.Direction)Random.Range(0, 4);
                var node = firstPass[Random.Range(0, firstPass.Count)];
                QuadTree[] neighbors = new QuadTree[length];
                for (int j = length - 1; j >= 0; --j)
                    neighbors[j] = node.GetEqualNeighbour(direction);
                if (neighbors.All(n => n != null))
                {
                    Rect newBounds = node.Boundary;
                    switch (direction)
                    {
                        case QuadTree.Direction.N:
                            newBounds.yMin = neighbors[neighbors.Length - 1].Boundary.yMin;
                            break;
                        case QuadTree.Direction.S:
                            newBounds.yMax = neighbors[neighbors.Length - 1].Boundary.yMax;
                            break;
                        case QuadTree.Direction.W:
                            newBounds.xMin = neighbors[neighbors.Length - 1].Boundary.xMin;
                            break;
                        case QuadTree.Direction.E:
                            newBounds.xMax = neighbors[neighbors.Length - 1].Boundary.xMax;
                            break;
                    }
                    if (!secondPass.Any(q => q.Boundary.Contains(newBounds.position) || q.Boundary.Contains(newBounds.position + newBounds.size) || q.Boundary.Contains(newBounds.center)))
                        secondPass.Add(new QuadTree(newBounds));
                }
            }
        }
        Debug.Log($"Second pass count - {secondPass.Count}");
        secondPass = secondPass.Distinct().ToList();
        Debug.Log($"Second pass count after distinction - {secondPass.Count}");

        //Merge();

        Place();
    }

    private void Merge()
    {
        mergePass = new List<QuadTree>();
        mergePass.AddRange(firstPass);
        for (int i = secondPass.Count - 1; i >= 0; --i)
        {
            selectedNode = secondPass[i];
            for (int j = firstPass.Count - 1; j >= 0; --j)
            {
                compareNode = firstPass[j];
                var result = selectedNode.Boundary.Contains(compareNode.Boundary.center);
                if (result)
                {
                    mergePass.Remove(compareNode);
                }
            }
        }
        mergePass.AddRange(secondPass);
    }

    private void Place()
    {
        //if (EditorApplication.isPlaying)
        {
            for (int i = firstPass.Count - 1; i >= 0; --i)
            {
                var node = firstPass[i];
                if (node.IsLeaf)
                {
                    if (node.Boundary.width <= room.width / 16f)
                    {
                        var section = sectionPrefabs.Where(s => s.size == Section.Size.Small).First();
                        if (Rand.Chance(section.placementProbability))
                        {
                            var pos = node.Boundary.position;
                            Instantiate(section.prefabs[Random.Range(0, section.prefabs.Count)], new Vector3(pos.x, roomAnchor.position.y, pos.y), Quaternion.identity, roomAnchor);
                        }
                    }
                    else if (node.Boundary.width <= room.width / 8f)
                    {
                        var section = sectionPrefabs.Where(s => s.size == Section.Size.Medium).First();
                        if (Rand.Chance(section.placementProbability))
                        {
                            var pos = node.Boundary.position;
                            Instantiate(section.prefabs[Random.Range(0, section.prefabs.Count)], new Vector3(pos.x, roomAnchor.position.y, pos.y), Quaternion.identity, roomAnchor);
                        }
                    }
                    else if (node.Boundary.width <= room.width / 4f)
                    {
                        var section = sectionPrefabs.Where(s => s.size == Section.Size.Large).First();
                        if (Rand.Chance(section.placementProbability))
                        {
                            var pos = node.Boundary.position;
                            Instantiate(section.prefabs[Random.Range(0, section.prefabs.Count)], new Vector3(pos.x, roomAnchor.position.y, pos.y), Quaternion.identity, roomAnchor);
                        }
                    }
                }
            }
        }
    }

    public void OnRegenerate() => Generate();

    private void OnDrawGizmos()
    {
        mergeCoroutine?.MoveNext();

        if (showFirstPass)
        {
            Gizmos.color = Color.white;
            root.DrawGizmos();
        }

        if (showSecondPass)
        {
            for (int i = secondPass.Count - 1; i >= 0; --i)
            {
                Gizmos.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), (float)i / secondPass.Count);
                secondPass[i].DrawGizmos();
            }
        }

        if (showMergePass)
        {
            Gizmos.color = Color.red;
            selectedNode?.DrawGizmos();

            Gizmos.color = Color.magenta;
            compareNode?.DrawGizmos();

            for (int i = mergePass.Count - 1; i >= 0; --i)
                mergePass[i].DrawGizmos();
        }
    }
}
