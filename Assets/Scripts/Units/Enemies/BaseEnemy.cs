using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
using Managers;
using Tiles;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Units.Enemies
{
    public class BaseEnemy : BaseUnit
    {
        public int ThreatLevel { get; set; } 
        protected int AlertDistance { get; set; }
        protected GameObject Player;
        protected List<Tile> pathToPlayer;

        public Item ItemToDrop;
        
        private bool _hasReachedPlayer;
        //protected bool AlertState = false;


        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (GameManager.Instance.GameState != GameState.EnemiesTurn) return;

            // Find player GameObject using its tag
            // = GameObject.FindGameObjectWithTag("Player");

            if (Player == null)
            {
                Debug.LogError("Player GameObject not found!");
                return;
            }

            if (!HasReachedPlayer(Player.transform) && GameManager.Instance.GameState == GameState.Gameplay)
            {
                _hasReachedPlayer = false;
                if (IsPlayerClose(Player.transform))// && !AlertState)
                {
                    // Trigger alert behavior for this enemy
                    //AlertState = true;
                    Debug.Log("ALERT!");
                    StartCoroutine(FollowPathAndNotify(FollowPath(pathToPlayer)));
                
                }   
                return;
            } if (HasReachedPlayer(Player.transform))
            {
                
                if (_hasReachedPlayer == false)
                {
                    BattleManager.Instance.onBattleInitiated?.Invoke(this);
            
                    GameManager.Instance.ChangeState(GameState.Battle);
            
                    Debug.Log("PLAYER'S REACHED!");  
                    
                    _hasReachedPlayer = true;
                }
                else
                {
                    GameManager.Instance.ChangeState(GameState.HeroesTurn);
                }
            }

            
            
            
        }

        public bool IsPlayerClose(Transform playerTransform)
        {
            if (playerTransform == null)
                return false;

            Vector2 enemyPosition = new Vector2(transform.position.x,
                transform.position.y);
            Vector2 playerPosition = new Vector2(playerTransform.position.x,
                playerTransform.position.y);


            Tile rightTile = GridManager.Instance.GetTileAtPosition(new Vector2(playerPosition.x+1, playerPosition.y));
            Tile leftTile = GridManager.Instance.GetTileAtPosition(new Vector2(playerPosition.x-1, playerPosition.y));
            Tile bottomTile = GridManager.Instance.GetTileAtPosition(new Vector2(playerPosition.x+1, playerPosition.y-1));
            Tile topTile = GridManager.Instance.GetTileAtPosition(new Vector2(playerPosition.x, playerPosition.y + 1));
            

            if (rightTile != null && rightTile.Walkable)
            {
                var rightTilePosition = rightTile.transform.position;
                playerPosition = new Vector2(rightTilePosition.x, rightTilePosition.y);
            } else if (leftTile != null && leftTile.Walkable)
            {
                var leftTilePosition = leftTile.transform.position;
                playerPosition = new Vector2(leftTilePosition.x, leftTilePosition.y);
            } else if (bottomTile != null && bottomTile.Walkable)
            {
                var bottomTilePosition = bottomTile.transform.position;
                playerPosition = new Vector2(bottomTilePosition.x, bottomTilePosition.y);
            }
            else
            {
                var topTilePosition = topTile.transform.position;
                playerPosition = new Vector2(topTilePosition.x, topTilePosition.y);
            }
            
         
            
            // Use A* to find a path from enemy to player
            pathToPlayer = GridManager.Instance.FindPath(enemyPosition, playerPosition);

            // If no path found, return false
            if (pathToPlayer == null)
                return false;

            return pathToPlayer.Count <= AlertDistance;
        }

        public bool HasReachedPlayer(Transform playerTransform)
        {
            Vector2 enemyPosition = new Vector2(Mathf.RoundToInt(transform.position.x),
                Mathf.RoundToInt(transform.position.y));
            Vector2 playerPosition = new Vector2(Mathf.RoundToInt(playerTransform.position.x),
                Mathf.RoundToInt(playerTransform.position.y));

            Vector2 difference = enemyPosition - playerPosition;
            
            // Check if the difference is one of the four direct neighbor offsets
            return (difference == Vector2.up) || 
                   (difference == Vector2.down) || 
                   (difference == Vector2.left) || 
                   (difference == Vector2.right);
            
        }
        
        // Coroutine to follow the path and notify when done
        protected IEnumerator FollowPathAndNotify(IEnumerator pathCoroutine)
        {
            yield return StartCoroutine(pathCoroutine);
            Debug.Log("done");
            GameManager.Instance.ChangeState(GameState.Battle); // Change back to hero's turn after enemy move
        }
    }
}
