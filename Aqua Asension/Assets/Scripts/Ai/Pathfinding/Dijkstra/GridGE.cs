using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGE : MonoBehaviour
{

    public int row = 5;
    public int column = 5;
    public float padding = 3f;
    public Transform nodePrefab;

    public List<Transform> grid = new List<Transform>();

   
    void Start()
    {
        this.generateGrid();
        this.generateNeighbours();
    }

   
    private void generateGrid()
    {

        int counter = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Transform node = Instantiate(nodePrefab, new Vector3((j * padding) + gameObject.transform.position.x, gameObject.transform.position.y, (i * padding) + gameObject.transform.position.z), Quaternion.identity);
                node.name = "node (" + counter + ")";
                if(counter % 7 == 0)
                {
                    node.GetComponent<NodeDJ>().walkable = false;
                    
                }


                grid.Add(node);
                counter++;
            }
        }


    }

    
    private void generateNeighbours()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            NodeDJ currentNode = grid[i].GetComponent<NodeDJ>();
            int index = i + 1;

           
            if (index % column == 1)
            {
               
                if (i + column < column * row)
                {
                    currentNode.addNeighbourNode(grid[i + column]);   
                }

                if (i - column >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - column]);  
                }
                currentNode.addNeighbourNode(grid[i + 1]);     
            }

           
            else if (index % column == 0)
            {
               
                if (i + column < column * row)
                {
                    currentNode.addNeighbourNode(grid[i + column]);   
                }

                if (i - column >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - column]);   
                }
                currentNode.addNeighbourNode(grid[i - 1]);    
            }

            else
            {
                
                if (i + column < column * row)
                {
                    currentNode.addNeighbourNode(grid[i + column]);   
                }

                if (i - column >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - column]);  
                }
                currentNode.addNeighbourNode(grid[i + 1]);     
                currentNode.addNeighbourNode(grid[i - 1]);    
            }

        }
    }
}











//https://github.com/N3evin/Dijkstra-Algorithm-Unity 