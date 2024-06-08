using UnityEngine;

namespace Tiles
{
    public class FloorTile : Tile
    {
        [SerializeField]
        private Sprite[] possibleSprites;

        private void Start()
        {
            AssignRandomSprite();
        }

        private void AssignRandomSprite()
        {
            if (possibleSprites != null && possibleSprites.Length > 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
            }
            else
            {
                Debug.LogWarning("No possible sprites assigned to FloorTile.");
            }
        }
    }
}
    