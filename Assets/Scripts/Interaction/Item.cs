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

    }
}