using System.Collections.Generic;
using System.Linq;
using Interaction;
using UnityEngine;

namespace Managers
{
    public class InteractableManager : MonoBehaviour
    {
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
            
            _items = Resources.LoadAll<Item>("Inventory/Items").ToList();
            
          
        }

        public void SpawnItems()
        {
            var itemCount = 2;
            var sword1Prefab = GetItem("Gilan's Sword");
            var spawnedSword1 = Instantiate(sword1Prefab);
            var randomSword1SpawnTile = GridManager.Instance.GetItemSpawnTile();
            
            randomSword1SpawnTile.SetInteractable(spawnedSword1);
            
            var helmet1Prefab = GetItem("Helmet of Protection");
            var spawnedHelmet1 = Instantiate(helmet1Prefab);
            var randomHelmet1SpawnTile = GridManager.Instance.GetItemSpawnTile();
            
            randomHelmet1SpawnTile.SetInteractable(spawnedHelmet1);
            
 
        }
        
        
        private ItemPickup GetItem(string itemName)
        {
            // go through the list (_units), we want all of the units according to the faction that we want, we randomly shuffle them, then select the first one and return its prefab
            return _items.Where(u => u.ItemName.Equals(itemName)).ToList().First().ItemPrefab;
        }
        
    }
}