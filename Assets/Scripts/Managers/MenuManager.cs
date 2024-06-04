using System;
using Tiles;
using TMPro;
using Units.Heroes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }


        private Tile _focusedTile;
        private Color _originalTileMenuColor;
        private Color _focusedTileMenuColor;
        
        [SerializeField] private GameObject _selectedHeroObject, _tileObject, _tileUnitObject; //TODO might create its own special class for this

        public bool InventoryOpen;
        
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

        private void Start()
        {
            _focusedTileMenuColor = Color.red;
            _focusedTileMenuColor.a = 0.7f;
            
            _originalTileMenuColor = _tileObject.GetComponent<Image>().color;
            _originalTileMenuColor.a = 0.3254902f;
            
            
        }

        private void Update()
        {
            CheckInventoryPanel();
        }

        private void CheckInventoryPanel()
        {
            GameState gameState = GameManager.Instance.GameState;

            if (Input.GetKeyDown("i"))
            {
                if (gameState == GameState.HeroesTurn || gameState == GameState.Gameplay || gameState == GameState.Inventory)
                {
                    if (!InventoryOpen)
                    {
                        UIManager.Instance.OpenInventoryPanel();
                        InventoryOpen = true;
                        GameManager.Instance.GameState = GameState.Inventory;
                        return;
                    }
                    
                    //Debug.Log("YAYYYYYYYYYYYYYYY");
                    
                    UIManager.Instance.CloseInventoryPanel();
                    InventoryOpen = false;
                    GameManager.Instance.GameState = GameState.HeroesTurn;
                    
                } 
            }
            
            
            
        }
        
        
        public void ShowSelectedHero(BaseHero hero)
        {
            if (hero == null)
            {
                _selectedHeroObject.SetActive(false); 
                return;
            }
            _selectedHeroObject.GetComponentInChildren<TMP_Text>().text = hero.UnitName;
            _selectedHeroObject.SetActive(true);
        }

        public void ShowTileInfo(Tile tile)
        {
            if (tile == null)
            {
                _tileObject.SetActive(false); 
                _tileUnitObject.SetActive(false); 
                return;
            }
                
            _tileObject.GetComponentInChildren<TMP_Text>().text = tile.TileName;
            _tileObject.SetActive(true);

            if (tile.OccupiedUnit)
            {
                
                _tileUnitObject.GetComponentInChildren<TMP_Text>().text = tile.OccupiedUnit.UnitName;
                _tileUnitObject.SetActive(true);
            }
            
            else if (tile.OccupiedInteractable)
            {
                _tileUnitObject.GetComponentInChildren<TMP_Text>().text = tile.OccupiedInteractable.Name;
                _tileUnitObject.SetActive(true);
            }
            
            
            /*
            if (_focusedTile != null)
            {
                _tileObject.GetComponentInChildren<TMP_Text>().text = _focusedTile.TileName;
                _tileObject.GetComponent<Image>().color = _focusedTileMenuColor;
                _tileObject.SetActive(true);

                if (_focusedTile.OccupiedUnit)
                {
                    _tileUnitObject.GetComponentInChildren<TMP_Text>().text = _focusedTile.OccupiedUnit.UnitName;
                    _tileUnitObject.GetComponent<Image>().color = _focusedTileMenuColor;
                    _tileUnitObject.SetActive(true);
                }
                
                else if (_focusedTile.OccupiedInteractable)
                {
                    _tileUnitObject.GetComponentInChildren<TMP_Text>().text = _focusedTile.OccupiedInteractable.Name;
                    _tileUnitObject.GetComponent<Image>().color = _focusedTileMenuColor;
                    _tileUnitObject.SetActive(true);
                }
                
                
            }

            else
            {
                
                
                if (tile == null)
                {
                    _tileObject.SetActive(false); 
                    _tileUnitObject.SetActive(false); 
                    return;
                }
                
                _tileObject.GetComponentInChildren<TMP_Text>().text = tile.TileName;
                _tileObject.SetActive(true);

                if (tile.OccupiedUnit)
                {
                
                    _tileUnitObject.GetComponentInChildren<TMP_Text>().text = tile.OccupiedUnit.UnitName;
                    _tileUnitObject.SetActive(true);
                }
            }
            */
            
           
            
        }

        public void FocusToTile(Tile focusedTile)
        {
            
            
            if (_focusedTile == null)
            {
                SetFocusedTile(focusedTile);
                StartCoroutine(_focusedTile.FlashTile());
                _focusedTile.SetFocusHighlightActive(true);
                _focusedTile.IsFlashing = true;
                StartCoroutine(_focusedTile.FlashTile());
 
            }
        
            /*
            _focusedTile.IsFlashing = false;
            StopCoroutine(_focusedTile.FlashTile());
            //_focusedTile.SetToOriginalColor();
            //_focusedTile.SetFocusHighlightActive(false);
            RemoveFocusedTile();
            */
            
        }

        public void DeFocusToTile()
        {
            if (_focusedTile)
            {
                _focusedTile.IsFlashing = false;
                StopCoroutine(_focusedTile.FlashTile());
                _focusedTile.SetToOriginalColor();
                _focusedTile.SetFocusHighlightActive(false);
                RemoveFocusedTile(); 
            }
            
            
        }
        
        

        private void SetFocusedTile(Tile focusedTile)
        {
            _focusedTile = focusedTile;
          
            //_tileObject.GetComponent<Image>().color = Color.red;
        }

        private void RemoveFocusedTile()
        {
            //_tileObject.GetComponent<Image>().color = _originalTileMenuColor;
            //_tileUnitObject.GetComponent<Image>().color = _originalTileMenuColor;
            _focusedTile = null;
        }
        
        
    }
}
