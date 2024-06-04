using System.Collections.Generic;
using System.Linq;
using Interaction;
using Units;
using UnityEngine;

namespace Managers
{
    public class InteractableManager : MonoBehaviour
    {
        
        public Interactable Focus;
        
        public static InteractableManager Instance { get; private set; }

        private List<Interactable> _interactables;

        private List<Item> _items;
        
        
        
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
            
            _items = Resources.LoadAll<Item>("Inventory/Items/Equipment").ToList();
            
          
        }

        public void SpawnItems()
        {
            var itemCount = 2;
            var sword1Prefab = GetItem("Dweemer Sword");
            var spawnedSword1 = Instantiate(sword1Prefab);
            var randomSword1SpawnTile = GridManager.Instance.GetItemSpawnTile();
            
            randomSword1SpawnTile.SetInteractable(spawnedSword1);
            
            var helmet1Prefab = GetItem("Sigmund's Helm");
            var spawnedHelmet1 = Instantiate(helmet1Prefab);
            var randomHelmet1SpawnTile = GridManager.Instance.GetItemSpawnTile();
            
            randomHelmet1SpawnTile.SetInteractable(spawnedHelmet1);
            
            var helmet2Prefab = GetItem("Orc Helmet");
            var spawnedHelmet2 = Instantiate(helmet2Prefab);
            var randomHelmet2SpawnTile = GridManager.Instance.GetItemSpawnTile();
            
            randomHelmet2SpawnTile.SetInteractable(spawnedHelmet2);
            
 
        }
        
        
        private ItemInteractable GetItem(string itemName)
        {
            // go through the list (_units), we want all of the units according to the faction that we want, we randomly shuffle them, then select the first one and return its prefab
            return _items.Where(u => u.ItemName.Equals(itemName)).ToList().First().ItemPrefab;
        }
        
        public void SetFocus(Interactable newFocus)
        {
            if (newFocus != Focus)
            {
                if (Focus != null)
                {
                    Focus.OnDeFocused(); 
                }
                
                Focus = newFocus;
            }
            
            
            newFocus.OnFocused();
        }

        public void RemoveFocus()
        {
            if (Focus != null)
                Focus.OnDeFocused();
            Focus = null;
        }
        
    }
}