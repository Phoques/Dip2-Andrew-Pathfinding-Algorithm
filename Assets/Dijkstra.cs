  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    public Node StartNode;
    public Node GoalNode;

    protected Node[] _nodesInScene;

    protected void GetAllNodes()
    {
        //This will search for all Node types in the scene, and store it into the variable.
        _nodesInScene = FindObjectsOfType<Node>();
    }


    public List<Node> FindShortestPath(Node start, Node goal)
    {
        GetAllNodes();

        //If RunAlgo Bool is true.
        if(RunAlgorithm(start, goal))
        {
            List<Node> results = new List<Node>();
            Node current = goal;

            do
            {
                results.Insert(0, current);

                //Populate results of the list here. Also using a getter setter here, cause _previousNode is private.
                current = current.PreviousNode;
            }
            while (current != null);
            return results;
        }

        return null;
    }

    //This is a protected accessor, with a return type of bool (instead of void) function.
    //That returns the bool as false;
    protected bool RunAlgorithm(Node start, Node goal)
    {
        return false;
    }

}
