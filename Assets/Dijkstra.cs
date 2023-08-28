using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DijkstraAlgo
{
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

        private void Start()
        {
            //RunAndDisplayPath();
            RunXTimes(1000);
        }

        public void RunAndDisplayPath()
        {

            List<Node> path = FindShortestPath(StartNode, GoalNode);

            for (int index = 0; index < path.Count -1; index++)
            {
                // Vector3.up literally translates to Vctor3(0,1,0);
                Debug.DrawLine(path[index].transform.position + Vector3.up, path[index + 1].transform.position + Vector3.up, Color.green, 10f);

                Debug.Log(path[index].name);
            }
            Debug.Log(path[path.Count - 1].name);
        }

        public void RunXTimes(int x)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int loop = 0; loop < x; loop++)
            {
                FindShortestPath(StartNode, GoalNode);
            }

            sw.Stop(); 
            Debug.Log(sw.ElapsedMilliseconds.ToString());
        }

        public List<Node> FindShortestPath(Node start, Node goal)
        {
            GetAllNodes();

            //If RunAlgo Bool is true.
            if (RunAlgorithm(start, goal))
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
            List<Node> unexplored = new List<Node>();

            Node startNode = null;
            Node goalNode = null;

            foreach (Node nodeInScene in _nodesInScene)
            {
                //Something here?
                nodeInScene.ResetNode();
                unexplored.Add(nodeInScene);

                if (start == nodeInScene)
                {
                    startNode = nodeInScene;
                }
                if (goal == nodeInScene)
                {
                    goalNode = nodeInScene;
                }
            }

            //If we cant find our start o end node, then we cant find a path.
            if (startNode == null || goalNode == null)
            {
                return false;
            }

            startNode.PathWeight = 0;

            while (unexplored.Count > 0)
            {
                //This is a little sort function to compare the two numbers inside the perenthesis / class.
                //Compare the A value of Pathweight and compare it to the B value.
                unexplored.Sort((a, b) => a.PathWeight.CompareTo(b.PathWeight));

                Node current = unexplored[0];
                unexplored.RemoveAt(0);

                foreach (Node neighbourNode in current.Neighbours)
                {
                    //Only explore unexplored nodes.
                    if (!unexplored.Contains(neighbourNode))
                    {
                        continue;
                    }

                    float neighbourWeight = Vector3.Distance(current.transform.position, neighbourNode.transform.position);
                    neighbourWeight += current.PathWeight;
                    //neighborWeight += penalty

                    //If the weght that we calculated above, is smaller then this is the new faster path
                    if (neighbourWeight < neighbourNode.PathWeight)
                    {
                        neighbourNode.PathWeight = neighbourWeight;
                        neighbourNode.PreviousNode = current;
                    }

                }

                //If we have completely explored the goal node it means we have made it to the end, and 'return true' stops the while loop.
                if (current == goalNode)
                {
                    return true; // Return true, returns the results and sorts it into the path.
                }

            }
            return false; // returns null.
        }



    }
}

