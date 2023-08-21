using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
   
    //A ist of nodes that are neighbours
    public List<Node> Neighbours;
    
    // variable = float with no end to the limit it can be.
    private float _pathWeight = float.PositiveInfinity;
    //I think this is going to store the node we have just moved from / have previously travelled on.
    private Node _previousNode;

    public Node PreviousNode
    {
        get { return _previousNode; }
        set { _previousNode = value; }
    }



    //This is needed to have a way to reset the node search, otherwise we could only search once, then woldnt be able to do it again as the
    //Node details would already be saved / remembered.
    public void ResetNode()
    {
        //Resets the pathweight which removes any previous weights added up.
        _pathWeight = float.PositiveInfinity;
        //Makes any of the previous nodes remembered to be forgotten.
        _previousNode = null;
    }

    //If anything in the inspector changes, this function runs. So, if we had a debug log in OnValidate, to say 'test'
    // If we changed anything on this script, in the inspector, for every single change 'test' will post into the console.
    private void OnValidate()
    {
        ValidateNeighbours();
    }

    private void ValidateNeighbours()
    {
            //For each node waypoint in the Neighbours List.
        foreach (Node waypoint in Neighbours)
        {
            //If the waypoint is null, skip over it to the next one.
            if(waypoint == null) continue;

            //Your neighbours, neighbour must contain yourself.
            //If you're not your neighbours, neighbour. (Im node 1, and my neighbor is node 2. Node 2 has two neighbours, if I am not one of those two, add me.) 
            if (!waypoint.Neighbours.Contains(this))
            {
                //Add yourself
                waypoint.Neighbours.Add(this);

            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Neighbours == null) return;

        Gizmos.color = Color.blue;

        foreach (Node waypoint in Neighbours)
        {
            if (waypoint == null) continue;
            //This draws a line from one node to another
            Gizmos.DrawLine(transform.position, waypoint.transform.position);
            //This draws a sphere around each node, at a 0.2f radius.
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }

}
