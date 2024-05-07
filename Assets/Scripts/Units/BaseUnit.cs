using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;
using Utilities;

namespace Units
{
    public class BaseUnit : MonoBehaviour
    {
        public string UnitName;
        public Tile OccupiedTile;
        public Faction Faction;
        
        public bool IsMoving { get; private set; }
        
        // the parameter path, should be a list containing Tiles, not Vector2's.
        public IEnumerator FollowPath(List<Tile> path)
        {
            IsMoving = true;
            Debug.Log(path.Count);

            foreach (Tile tile in path)
            {
                if (tile != OccupiedTile) // Ensure the tile is not the one we're already on
                {
                    yield return StartCoroutine(MoveToTile(tile, 0.5f)); // Use the adjusted method that handles tile transition
                }
            }

            IsMoving = false;
        }

        // Move to a specific position (not so good)
        private IEnumerator MoveTo(Vector2 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(targetPosition.x, targetPosition.y, startPosition.z); // Maintain z-coordinate
            float elapsed = 0;

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;  // Wait until the next frame
            }

            transform.position = endPosition; // Ensure the unit exactly reaches the target position
        }
        
        
        // Move to a specific tile (much better)
        public IEnumerator MoveToTile(Tile targetTile, float baseDuration)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = targetTile.transform.position;
            bool isDiagonal = Mathf.Abs(targetTile.transform.position.x - startPosition.x) == Mathf.Abs(targetTile.transform.position.y - startPosition.y);
            float duration = isDiagonal ? baseDuration * 1.414f  : baseDuration; // Diagonal movements are longer by the factor of sqrt(2)
            float elapsed = 0;

            while (elapsed < duration)
            {
                transform.position = Vector3.MoveTowards(startPosition, endPosition, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition;
            if (OccupiedTile != null) OccupiedTile.OccupiedUnit = null;
            OccupiedTile = targetTile;
            targetTile.OccupiedUnit = this;
            IsMoving = false;
        }
        
        
    }
}