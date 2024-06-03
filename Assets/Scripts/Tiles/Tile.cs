using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
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


        [SerializeField] protected Color _originalColor;
        [SerializeField] private Color _flashColor = Color.red;
        [SerializeField] protected SpriteRenderer _renderer;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private GameObject _focusHighlight;
        [SerializeField] private bool _isWalkable;

        public BaseUnit OccupiedUnit;
        public Interactable OccupiedInteractable;
        
        public bool IsFlashing = false; 
        
        // Add if it has an interactable on it
        
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

        private void OnMouseOver()
        {
            // Check if the right mouse button is clicked while the cursor is over this tile
            if (Input.GetMouseButtonDown(1))
            {
                //OnRightMouseDown();
            }
        }
        
        /*
        private void OnRightMouseDown()
        {
            Debug.Log("Yes, On right mouse down");
            MenuManager.Instance.FocusToTile(this);
            MenuManager.Instance.ShowTileInfo(this);
            
        }
        
        */

        // MouseDown is only cared about when the GameState is Player's (Hero's) turn
        private void OnMouseDown()
        {
            if(GameManager.Instance.GameState != GameState.HeroesTurn) return;

            // Means that there is a unit on the tile
            if (OccupiedUnit != null)
            {   
                Debug.Log(OccupiedUnit.UnitName);
                Debug.Log(OccupiedUnit.Faction);
                if (OccupiedUnit.Faction == Faction.Hero)
                {
                    Debug.Log("yes!");
                    UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                }
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
                
                BaseHero selectedHero = UnitManager.Instance.SelectedHero;
                if (selectedHero != null && Walkable && !selectedHero.IsMoving)
                {
                    Debug.Log("Yes you can move!");
                    MenuManager.Instance.FocusToTile(this);
                    // A* Path Finding
                    Tile startTile = UnitManager.Instance.SelectedHero.OccupiedTile;
                    Tile endTile = this; 

                    List<Tile> path = GridManager.Instance.FindPath(startTile.transform.position, endTile.transform.position);
                    if (path != null)
                    {
                        StartCoroutine(UnitManager.Instance.SelectedHero.FollowPath(path));
                    }
                }
                
                else if (selectedHero != null && selectedHero.IsMoving)
                {
                    StartCoroutine(UnitManager.Instance.SelectedHero.StopMovement());
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
        
        public IEnumerator FlashTile()
        {
            float flashDuration = 1.5f;  // Total duration for one complete flash cycle (to red and back to original color)
            float halfFlashDuration = flashDuration / 2f;
            while (IsFlashing)
            {
                // Interpolate to flash color
                float elapsed = 0f;
                while (elapsed < halfFlashDuration)
                {
                    _renderer.color = Color.Lerp(_originalColor, _flashColor, elapsed / halfFlashDuration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }

                // Interpolate back to original color
                elapsed = 0f;
                while (elapsed < halfFlashDuration)
                {
                    _renderer.color = Color.Lerp(_flashColor, _originalColor, elapsed / halfFlashDuration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
            }
        }

        public void SetToOriginalColor()
        {
            _renderer.color = _originalColor;
        }
        
        public void SetFocusHighlightActive(bool activated)
        {
            _focusHighlight.SetActive(activated);
        }
        
    }
}