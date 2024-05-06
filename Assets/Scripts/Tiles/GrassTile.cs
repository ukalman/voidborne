using UnityEngine;

namespace Tiles
{
    public class GrassTile : Tile
    {
        [SerializeField] private Color _baseColor, _offsetColor;

        public override void Init(int x, int y)
        {
            // var isOffset = (x % 2 == 0 && y % 2 != 0) ||(x % 2 != 0 && y % 2 == 0); 
            var isOffset = (x + y) % 2 == 1; // is x even and y is not even or x is not even and y is even
            _renderer.color = isOffset ? _offsetColor : _baseColor;
        }
        
    }
}