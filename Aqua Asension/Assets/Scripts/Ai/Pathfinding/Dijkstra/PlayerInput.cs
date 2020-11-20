using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour {

    private Transform node;
    private Transform startNode;
    private Transform endNode;
    private List<Transform> blockPath = new List<Transform>();

	
	void Update () {
        mouseInput();
    }
    
   
    private void mouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

            
            this.colorBlockPath();
            this.updateNodeColor();

            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Node")
            {
               
                Renderer rend;
                if (node != null)
                {
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.white;
                }

               
                node = hit.transform;

                
                rend = node.GetComponent<Renderer>();
                rend.material.color = Color.green;

            }
        }
    }

   
    public void btnStartNode()
    {
        if (node != null)
        {
            NodeDJ n = node.GetComponent<NodeDJ>();

            
            if (n.isWalkable())
            {
                
                if (startNode == null)
                {
                    Renderer rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }
                else
                {
                    
                    Renderer rend = startNode.GetComponent<Renderer>();
                    rend.material.color = Color.white;

                  
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }

                startNode = node;
                node = null;
            }
        }
    }

   
    public void btnEndNode()
    {
        if (node != null)
        {
            NodeDJ n = node.GetComponent<NodeDJ>();

           
            if (n.isWalkable())
            {
                
                if (endNode == null)
                {
                    Renderer rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.cyan;
                }
                else
                {
                    
                    Renderer rend = endNode.GetComponent<Renderer>();
                    rend.material.color = Color.white;

                    
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.cyan;
                }

                endNode = node;
                node = null;
            }
        }
    }

    
    public void btnFindPath()
    {   
        
        if (startNode != null && endNode != null)
        {
            
            ShortestPath finder = gameObject.GetComponent<ShortestPath>();
            List<Transform> paths = finder.findShortestPath(startNode, endNode);

           
            foreach (Transform path in paths)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.color = Color.red;
            }
        }
    }

   
    public void btnBlockPath()
    {
        if (node != null)
        {
            
            Renderer rend = node.GetComponent<Renderer>();
            rend.material.color = Color.black;

           
            NodeDJ n = node.GetComponent<NodeDJ>();
            n.setWalkable(false);

           
            blockPath.Add(node);

            
            if (node == startNode)
            {
                startNode = null;
            }

           
            if (node == endNode)
            {
                endNode = null;
            }

            node = null;
        }

        
        UnitSelectionComponent selection = gameObject.GetComponent<UnitSelectionComponent>();
        List<Transform> selected = selection.getSelectedObjects();

        foreach(Transform nd in selected)
        {
            
            Renderer rend = nd.GetComponent<Renderer>();
            rend.material.color = Color.black;

            
            NodeDJ n = nd.GetComponent<NodeDJ>();
            n.setWalkable(false);

           
            blockPath.Add(nd);

           
            if (nd == startNode)
            {
                startNode = null;
            }

           
            if (nd == endNode)
            {
                endNode = null;
            }
        }

        selection.clearSelections();
    }

    
    public void btnUnblockPath()
    {
        if (node != null)
        {
           
            Renderer rend = node.GetComponent<Renderer>();
            rend.material.color = Color.white;

            
            NodeDJ n = node.GetComponent<NodeDJ>();
            n.setWalkable(true);

            
            blockPath.Remove(node);

            node = null;
        }

        
        UnitSelectionComponent selection = gameObject.GetComponent<UnitSelectionComponent>();
        List<Transform> selected = selection.getSelectedObjects();

        foreach (Transform nd in selected)
        {
            
            Renderer rend = nd.GetComponent<Renderer>();
            rend.material.color = Color.white;

           
            NodeDJ n = nd.GetComponent<NodeDJ>();
            n.setWalkable(true);

            
            blockPath.Remove(nd);
        }

        selection.clearSelections();
    }

   
    public void btnClearBlock()
    {   
        
        foreach(Transform path in blockPath)
        {   
           
            NodeDJ n = path.GetComponent<NodeDJ>();
            n.setWalkable(true);

           
            Renderer rend = path.GetComponent<Renderer>();
            rend.material.color = Color.white;

        }
        
        blockPath.Clear();
    }

   
    public void btnRestart()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

   
    public void colorBlockPath()
    {
        foreach(Transform block in blockPath)
        {
            Renderer rend = block.GetComponent<Renderer>();
            rend.material.color = Color.black;
        }
    }

    
    private void updateNodeColor()
    {
        if (startNode != null)
        {
            Renderer rend = startNode.GetComponent<Renderer>();
            rend.material.color = Color.blue;
        }

        if (endNode != null)
        {
            Renderer rend = endNode.GetComponent<Renderer>();
            rend.material.color = Color.cyan;
        }
    }

}
