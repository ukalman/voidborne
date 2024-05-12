using System.Collections;
using System.Collections.Generic;
using Managers;
using Tiles;
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
        //protected bool AlertState = false;
        
        
        private void Update()
        {
            // Find player GameObject using its tag
            Player = GameObject.FindGameObjectWithTag("Player");

            if (Player == null)
            {
                Debug.LogError("Player GameObject not found!");
                return;
            }

            // Iterate through enemies and check if player is close
            foreach (var enemy in UnitManager.Instance._enemies)
            {
                if (enemy.IsPlayerClose(Player.transform))// && !AlertState)
                {
                    // Trigger alert behavior for this enemy
                    //AlertState = true;
                    Debug.Log("ALERT!");
                    StartCoroutine(FollowPathAndNotify(enemy.FollowPath(pathToPlayer)));
                }
            }
        }

        public bool IsPlayerClose(Transform playerTransform)
        {
            if (playerTransform == null)
                return false;

            Vector2 enemyPosition = new Vector2(Mathf.RoundToInt(transform.position.x),
                Mathf.RoundToInt(transform.position.y));
            Vector2 playerPosition = new Vector2(Mathf.RoundToInt(playerTransform.position.x),
                Mathf.RoundToInt(playerTransform.position.y));

            // Use A* to find a path from enemy to player
            pathToPlayer = GridManager.Instance.FindPath(enemyPosition, playerPosition);

            // If no path found, return false
            if (pathToPlayer == null)
                return false;

            return pathToPlayer.Count <= AlertDistance;
        }
        
        // Coroutine to follow the path and notify when done
        protected IEnumerator FollowPathAndNotify(IEnumerator pathCoroutine)
        {
            yield return StartCoroutine(pathCoroutine);
            Debug.Log("done");
            GameManager.Instance.ChangeState(GameState.Battle);
        }
    }
}