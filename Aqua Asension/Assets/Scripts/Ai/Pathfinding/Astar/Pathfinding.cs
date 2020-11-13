using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform StartPosition;
    public Transform TargetPosition;
   
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void Update()
    {
        FindPath(StartPosition.position, TargetPosition.position);
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_StartPos);
        Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> CloseList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while(OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {

                if (OpenList[i].FCost < CurrentNode.FCost|| OpenList[i].FCost == CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost)
                {
                    CurrentNode = OpenList[i];
                }

            }

            OpenList.Remove(CurrentNode);
            CloseList.Add(CurrentNode);
            if(CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }
            foreach (Node NeighborNode in grid.GetNeighboringNodes(CurrentNode))
            {
                if ( !NeighborNode.Wall|| CloseList.Contains(NeighborNode))
                {
                    continue;
                }
                int MoveCosts = CurrentNode.gCost + GetManhattenDistance(CurrentNode, NeighborNode);

                if(MoveCosts < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                {

                    NeighborNode.gCost = MoveCosts;
                    NeighborNode.hCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighborNode))
                    {

                        OpenList.Add(NeighborNode);

                    }

                }

            }


        }
    }


    void GetFinalPath(Node a_StartNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node currentNode = a_EndNode;

        while (currentNode != a_StartNode)
        {
            FinalPath.Add(currentNode);
            currentNode = currentNode.Parent;
            OnDrawGizmos();

            FinalPath.Reverse();
            grid.FinalPath = FinalPath;


 void OnDrawGizmos()
        {
                Gizmos.DrawCube(currentNode.Position, new Vector3(grid.gridWorldSize.x, 1, grid.gridWorldSize.y));
                Gizmos.color = Color.red;
            Gizmos.DrawCube(currentNode.Position, Vector3.one * (grid.nodeRadious - grid.Distance));
        }


        }
    }
    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {

        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;
    }
        
       
    
}
//https://youtu.be/AKKpPmxx07w