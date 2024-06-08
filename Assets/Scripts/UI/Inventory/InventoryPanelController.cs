using System;
using UnityEngine;
using Interaction;
using Unity.VisualScripting;

namespace UI.Inventory
{
    
    public class InventoryPanelController : MonoBehaviour
    {
        public Transform ItemsParent;

        public Transform ArmorParent;
        
        public Transform WeaponParent;
        
        private InventorySlot[] _slots;
        
        private EquipmentSlot[] _armorEquipmentSlots;
        
        private EquipmentSlot[] _weaponEquipmentSlots;
        
        private Interaction.Inventory _inventory;

        private void Start()
        {
            _inventory = Interaction.Inventory.Instance;
            _inventory.OnItemChangedCallback += UpdateUI;

            EquipmentManager.Instance.onEquipmentChangedUI += UpdateEquipmentUI;
            
  
            _slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
            _armorEquipmentSlots = ArmorParent.GetComponentsInChildren<EquipmentSlot>();
            _weaponEquipmentSlots = WeaponParent.GetComponentsInChildren<EquipmentSlot>();
            
            UpdateUI();
            
        }

        private void OnDestroy()
        {
            _inventory.OnItemChangedCallback -= UpdateUI;
            EquipmentManager.Instance.onEquipmentChangedUI -= UpdateEquipmentUI;
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
            
            UpdateEquipmentUI();
            
            
        }

        void UpdateEquipmentUI()
        {
            int armorSlotsLength = _armorEquipmentSlots.Length;
            int weaponSlotsLength = _weaponEquipmentSlots.Length;
            
            Debug.Log("Armor slots length: " + armorSlotsLength);
            Debug.Log("Weapon slots length: " + weaponSlotsLength);
            
            for (int i = 0; i < armorSlotsLength; i++)
            {
                var currentEquipment = EquipmentManager.Instance.CurrentEquipment[i];
                
                if (currentEquipment != null)
                {
                    _armorEquipmentSlots[i].AddEquipment(currentEquipment);
                }

                else
                {
                    Debug.Log("Armor equipment nulL!");
                    _armorEquipmentSlots[i].ClearSlot();   
                }
            }

            for (int i = armorSlotsLength; i < weaponSlotsLength + armorSlotsLength; i++)
            {
                var currentEquipment = EquipmentManager.Instance.CurrentEquipment[i];

                Debug.Log("i: " + i);
                
                if (currentEquipment != null)
                {
                    _weaponEquipmentSlots[i - armorSlotsLength].AddEquipment(currentEquipment);
                }

                else
                {
                    Debug.Log("yes it's null!");
                    _weaponEquipmentSlots[i - armorSlotsLength].ClearSlot();
                }
            }
        }
        
        
      
    }
}