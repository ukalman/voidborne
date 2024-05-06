using System;
using Tiles;
using TMPro;
using Units.Heroes;
using UnityEngine;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }

        [SerializeField] private GameObject _selectedHeroObject, _tileObject, _tileUnitObject; //TODO might create its own special class for this
        
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
        }
        
        
    }
}
