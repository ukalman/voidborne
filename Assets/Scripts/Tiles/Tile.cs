using System;
using Managers;
using Units;
using Units.Enemies;
using Units.Heroes;
using UnityEngine;
using Utilities;

namespace Tiles
{
    public abstract class Tile : MonoBehaviour
    {
        public string TileName;
        
        [SerializeField] protected SpriteRenderer _renderer;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private bool _isWalkable;

        public BaseUnit OccupiedUnit;
        public bool Walkable => _isWalkable && OccupiedUnit == null; // We don't wanna go on a tile that is already occupied, and not walkable.
        
        public virtual void Init(int x, int y)
        {
            
        }

        private void OnMouseEnter()
        {
            _highlight.SetActive(true);
            MenuManager.Instance.ShowTileInfo(this);
        }

        private void OnMouseExit()
        {
            _highlight.SetActive(false);
            MenuManager.Instance.ShowTileInfo(null);
        }

        // MouseDown is only cared about when the GameState is Player's (Hero's) turn
        private void OnMouseDown()
        {
            if(GameManager.Instance.GameState != GameState.HeroesTurn) return;

            if (OccupiedUnit != null)
            {
                if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                else // TODO for now, else condition means an Enemy is selected, but change it later for friendly NPC's
                {
                    if (UnitManager.Instance.SelectedHero != null)
                    {
                        var enemy = (BaseEnemy)OccupiedUnit;
                        // TODO Attack logic?? Maybe UnitManager.Instance.SelectedHero.Attack(enemy) or enemy.TakeDamage()
                        Destroy(enemy.gameObject); // Temporary solution (just destroy the enemy)
                        UnitManager.Instance.SetSelectedHero(null); // deselect 
                    }
                }
            }
            else // Player (Hero) move logic
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    if (Walkable)
                    {
                        SetUnit(UnitManager.Instance.SelectedHero);
                        UnitManager.Instance.SetSelectedHero(null); // deselect
                    }
                    
                }
            }
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