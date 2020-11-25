using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using AquaAscension.Utils;

namespace AquaAscension
{
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
            if (GUILayout.Button("Clear"))
                roomGen.ClearRoom();
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
        public bool showLeaves = false;
        public bool showSecondPass = false;
        public bool showMergePass = false;

        [SerializeField] private QuadTree root;
        [SerializeField] private List<QuadTree> firstPass;
        [SerializeField] private List<QuadTree> secondPass;
        [SerializeField] private List<QuadTree> mergePass;

        private QuadTree selectedNode;
        private QuadTree compareNode;
        private IEnumerator mergeCoroutine;

        public void OnRegenerate() => Generate();

        private void Start() => Generate();

        private void Generate()
        {
            //TODO Get the Room size...
            root = new QuadTree(new Rect(room.x, room.y, room.width, room.height));
            root.Generate(SUBDIVIDE_COUNT);
            firstPass.Clear();
            firstPass = root.LevelTraversal();

            Debug.Log($"First pass count - {firstPass.Count}");

            #region MFK
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
            ClearRoom();
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

        public void ClearRoom()
        {
            while (roomAnchor.childCount > 0)
            {
                foreach (Transform child in roomAnchor)
                {
#if UNITY_EDITOR
                    if (!EditorApplication.isPlaying)
                        DestroyImmediate(child.gameObject);
                    else
                        Destroy(child.gameObject);
#else
                Destroy(child.gameObject);
#endif
                }
            }
        }

        private void OnDrawGizmos()
        {
            mergeCoroutine?.MoveNext();

            if (showFirstPass)
            {
                Gizmos.color = Color.white;
                for (int i = firstPass.Count - 1; i >= 0; --i)
                {
                    firstPass[i].DrawGizmos();
                }
            }

            if (showLeaves)
            {
                Gizmos.color = Color.green;
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
}