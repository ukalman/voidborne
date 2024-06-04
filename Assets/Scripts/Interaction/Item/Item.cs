using UnityEngine;

namespace Interaction
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string ItemName = "New Item";
        public Sprite icon = null;
        public bool isDefaultItem = false;
        public ItemInteractable ItemPrefab;

        public virtual void Use()
        {
            // Use the item
            // Something might happen
            Debug.Log("Using " + ItemName);
        }

        public void RemoveFromInventory()
        {
            Inventory.Instance.Remove(this);
        }
        
        
    }
}