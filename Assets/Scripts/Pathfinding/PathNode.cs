using UnityEngine;

namespace Pathfinding
{
    public class PathNode
    {
        public Vector2 Position;
        public float Cost;  // Cost from start node
        public float Heuristic;  // Estimated cost to target node
        public float TotalCost => Cost + Heuristic;  // Total cost (f = g + h in A* terminology)
        public PathNode CameFrom;  // Node from which this node was reached
        public bool Walkable;
    }
}