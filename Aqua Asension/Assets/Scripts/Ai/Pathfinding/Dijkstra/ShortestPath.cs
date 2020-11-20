using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestPath : MonoBehaviour
{

    private GameObject[] nodes;

    
    public List<Transform> findShortestPath(Transform start, Transform end)
    {

        nodes = GameObject.FindGameObjectsWithTag("Node");

        List<Transform> result = new List<Transform>();
        Transform node = DijkstrasAlgo(start, end);

       
        while (node != null)
        {
            result.Add(node);
            NodeDJ currentNode = node.GetComponent<NodeDJ>();
            node = currentNode.getParentNode();
        }

       
        result.Reverse();
        return result;
    }

   
    private Transform DijkstrasAlgo(Transform start, Transform end)
    {
        double startTime = Time.realtimeSinceStartup;

        
        List<Transform> unexplored = new List<Transform>();

       
        foreach (GameObject obj in nodes)
        {
            NodeDJ n = obj.GetComponent<NodeDJ>();
            if (n.isWalkable())
            {
                n.resetNode();
                unexplored.Add(obj.transform);
            }
        }

       
        NodeDJ startNode = start.GetComponent<NodeDJ>();
        startNode.setWeight(0);

        while (unexplored.Count > 0)
        {
            
            unexplored.Sort((x, y) => x.GetComponent<NodeDJ>().getWeight().CompareTo(y.GetComponent<NodeDJ>().getWeight()));

           
            Transform current = unexplored[0];

            
            unexplored.Remove(current);

            NodeDJ currentNode = current.GetComponent<NodeDJ>();
            List<Transform> neighbours = currentNode.getNeighbourNode();
            foreach (Transform neighNode in neighbours)
            {
                NodeDJ node = neighNode.GetComponent<NodeDJ>();

                
                if (unexplored.Contains(neighNode) && node.isWalkable())
                {
                   
                    float distance = Vector3.Distance(neighNode.position, current.position);
                    distance = currentNode.getWeight() + distance;

                    
                    if (distance < node.getWeight())
                    {
                       
                        node.setWeight(distance);
                        node.setParentNode(current);
                    }
                }
            }

        }

        double endTime = (Time.realtimeSinceStartup - startTime);
        print("Compute time: " + endTime);

        print("Path completed!");

        return end;
    }

}
