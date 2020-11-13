using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPosition;
    public LayerMask WallMask;
    public Vector2 gridWorldSize;
    public float nodeRadious;
    public float Distance;

    Node[,] grid;
    public List<Node> FinalPath;
    float nodeDiamiter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiamiter = nodeRadious * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiamiter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiamiter);
        CreateGrid();
    }

    void CreateGrid()
    {

        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int y = 0; y< gridSizeY; y++) { 
        for(int x = 0; x < gridSizeX; x++)
        {

                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiamiter + nodeRadious) + Vector3.forward * (y * nodeDiamiter + nodeRadious);
                bool Wallc = true;

                if (Physics.CheckSphere(worldPoint, nodeRadious, WallMask))
                {

                    Wallc = false;

                }

                grid[x, y] = new Node(Wallc, worldPoint, x, y);

        }

        }
    }
   public Node NodeFromWorldPosition(Vector3 a_worldPosition)
    {
        float xpoint = ((a_worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((a_worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }

    public List<Node> GetNeighboringNodes(Node a_Node)
    {

        List<Node> NeighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //right
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;

        if (xCheck >= 0 && xCheck< gridSizeX)
        {

            if (yCheck >= 0 && yCheck < gridSizeY)
            {

                NeighboringNodes.Add(grid[xCheck, yCheck]);

            }

        }
        //Left
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {

            if (yCheck >= 0 && yCheck < gridSizeY)
            {

                NeighboringNodes.Add(grid[xCheck, yCheck]);

            }

        }

        //Top
        xCheck = a_Node.gridX ;
        yCheck = a_Node.gridY+ 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {

            if (yCheck >= 0 && yCheck < gridSizeY)
            {

                NeighboringNodes.Add(grid[xCheck, yCheck]);

            }

        }

        //Bottom
        xCheck = a_Node.gridX ;
        yCheck = a_Node.gridY- 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {

            if (yCheck >= 0 && yCheck < gridSizeY)
            {

                NeighboringNodes.Add(grid[xCheck, yCheck]);

            }

        }
        return NeighboringNodes;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.Wall)
                {

                    Gizmos.color = Color.white;

                }
                else
                {
                    Gizmos.color = Color.yellow;
                }
              //  if(FinalPath != null)
               // {
               //     Gizmos.color = Color.red;
               // }
                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiamiter - Distance));
            }
        }
    }


}
//https://youtu.be/AKKpPmxx07w