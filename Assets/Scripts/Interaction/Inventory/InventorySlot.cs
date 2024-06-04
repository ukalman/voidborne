using UnityEngine;
using UnityEngine.UI;

namespace Interaction
{
    public class InventorySlot : MonoBehaviour
    {
        public Image Icon;
        public Button removeButton;
        
        private Item _item;

        public void AddItem(Item newItem)
        {
            _item = newItem;
            Debug.Log("New Item's name: " + newItem.name);
            Debug.Log("Item's name: " + _item.name);
            
            if(_item.icon == null) Debug.Log("Yes, item's icon is null.");
            
            Icon.sprite = _item.icon;
            Icon.enabled = true;
            removeButton.interactable = true;
        }

        public void ClearSlot()
        {
            _item = null;
            Icon.sprite = null;
            Icon.enabled = false;
            removeButton.interactable = false;
        }

        public void OnRemoveButton()
        {
            Inventory.Instance.Remove(_item);
        }

        public void UseItem()
        {
            if (_item != null)
            {
                _item.Use();
            }
        }
        
    }
}
