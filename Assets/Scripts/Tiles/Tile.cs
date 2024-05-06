using System;
using Units;
using UnityEngine;

namespace Tiles
{
    public abstract class Tile : MonoBehaviour
    {
        
        [SerializeField] protected SpriteRenderer _renderer;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private bool _isWalkable;

        public BaseUnit OccupiedUnit;
        public bool Walkable => _isWalkable && OccupiedUnit == null; // We don't wanna go on a tile that is already occupied, and not walkable.
        
        public virtual void Init(int x, int y)
        {
            
        }

        void OnMouseEnter()
        {
            _highlight.SetActive(true);
        }

        private void OnMouseExit()
        {
            _highlight.SetActive(false);
        }

        public void SetUnit(BaseUnit unit)
        {
            if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null; // deallocate the tile
            unit.transform.position = transform.position;
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
        }
    }
}