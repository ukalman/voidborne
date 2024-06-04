using System;
using UnityEngine;
using Interaction;

namespace UI.Inventory
{
    
    public class InventoryPanelController : MonoBehaviour
    {
        public Transform ItemsParent;

        private InventorySlot[] _slots;
        
        private Interaction.Inventory _inventory;

        private void Start()
        {
            _inventory = Interaction.Inventory.Instance;
            _inventory.OnItemChangedCallback += UpdateUI;

            _slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
        }

        void UpdateUI()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (i < _inventory.Items.Count)
                {
                    _slots[i].AddItem(_inventory.Items[i]);
                }
                else
                {
                    _slots[i].ClearSlot();
                }
            }
            
        }
        
    }
}