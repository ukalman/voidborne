using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Tiles;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;


namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance { get; private set; }
        
        [SerializeField] private int _width, _height;

        [SerializeField] private Tile _grassTile, _mountainTile;

        [SerializeField] private Transform _cam;

        [SerializeField] private Transform _tilesParent;  
        
        private Dictionary<Vector2, Tile> _tiles; // key is position, value is the Tile itself
        private Dictionary<Vector2, PathNode> _pathNodes;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GenerateGrid()
        {
            _tiles = new Dictionary<Vector2, Tile>();
            _pathNodes = new Dictionary<Vector2, PathNode>();
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                    var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity, _tilesParent);
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.Init(x, y);

                    _tiles[new Vector2(x, y)] = spawnedTile;
                    _pathNodes[new Vector2(x, y)] = new PathNode { Position = new Vector2(x, y), Walkable = spawnedTile.Walkable };
                }
            }

            _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
            
            GameManager.Instance.ChangeState(GameState.SpawnHeroes);
            
        }

        public Tile GetHeroSpawnTile()
        {
            // Spawn hero on the left (left side of the map) on a Walkable tile
            return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
        }
        
        public Tile GetEnemySpawnTile()
        {
            // Spawn enemy on the right (right side of the map) on a Walkable tile
            return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
        }
        
        public Tile GetTileAtPosition(Vector2 pos)
        {
            return _tiles.GetValueOrDefault(pos);
        }
        
        public bool AreTilesAdjacent(Vector2 pos1, Vector2 pos2)
        {
            return Math.Abs(pos1.x - pos2.x) <= 1 && Math.Abs(pos1.y - pos2.y) <= 1 && (pos1 != pos2);
        }
        
        public List<Tile> FindPath(Vector2 start, Vector2 end)
        {
            var openSet = new PriorityQueue<PathNode>();
            foreach (var node in _pathNodes.Values)
            {
                node.Cost = float.MaxValue;
                node.CameFrom = null;
            }

            var startNode = _pathNodes[start];
            var endNode = _pathNodes[end];
            startNode.Cost = 0;
            startNode.Heuristic = Vector2.Distance(start, end);
            openSet.Enqueue(startNode, startNode.TotalCost);

            while (openSet.Count > 0)
            {
                var currentNode = openSet.Dequeue();
                if (currentNode == endNode)
                {
                    return ReconstructPath(endNode);
                }

                foreach (var neighbor in GetNeighbors(currentNode.Position))
                {
                    var neighborNode = _pathNodes[neighbor];
                    if (!neighborNode.Walkable)
                        continue;

                    float tentativeCost = currentNode.Cost + Vector2.Distance(currentNode.Position, neighbor);
                    if (tentativeCost < neighborNode.Cost)
                    {
                        neighborNode.CameFrom = currentNode;
                        neighborNode.Cost = tentativeCost;
                        neighborNode.Heuristic = Vector2.Distance(neighbor, end);
                        if (!openSet.Contains(neighborNode))
                            openSet.Enqueue(neighborNode, neighborNode.TotalCost);
                    }
                }
            }

            return null;  // No path found
        }

        private List<Tile> ReconstructPath(PathNode endNode)
        {
            var path = new List<Tile>();
            var currentNode = endNode;
            while (currentNode != null)
            {
                path.Add(_tiles[currentNode.Position]);
                currentNode = currentNode.CameFrom;
            }
            path.Reverse();
            return path;
        }

        private List<Vector2> GetNeighbors(Vector2 position)
        {
            var positions = new List<Vector2>
            {
                new Vector2(position.x - 1, position.y),
                new Vector2(position.x + 1, position.y),
                new Vector2(position.x, position.y - 1),
                new Vector2(position.x, position.y + 1)
            };
            return positions.Where(p => _pathNodes.ContainsKey(p)).ToList();
        }
        
        
        
    }
}