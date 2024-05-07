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
        
        public IEnumerator FollowPath(List<Vector2> path)
        {
            Debug.Log(path);
            IsMoving = true;

            foreach (Vector2 position in path)
            {
                // Wait until the unit reaches each point on the path
                yield return StartCoroutine(MoveTo(position, 0.5f));
            }

            IsMoving = false;
        }

        // Move to a specific position
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
        
        
        /* Old Code
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
        */
        
    }
}