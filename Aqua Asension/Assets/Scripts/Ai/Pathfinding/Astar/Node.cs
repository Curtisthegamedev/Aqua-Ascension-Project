using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public int gridX;
        public int gridY;
    public bool Wall;
    public Vector3 Position;

    public Node Parent;
    public int gCost;
    public int hCost;
    public int FCost { get { return gCost + hCost; } }

    public Node(bool a_Wall, Vector3 a_Pos, int a_gridX, int a_gridY)
    {

        Wall = a_Wall; //checks for Node obstruction
        Position = a_Pos; //world position of Node
        gridX = a_gridX;//xposition in nod array
        gridY = a_gridY;//y position in node array

    }



}
//https://youtu.be/AKKpPmxx07w