using System;
using UnityEngine;
using Interaction;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using Utilities;

namespace UI.Inventory
{
    
    public class InventoryPanelController : MonoBehaviour
    {
        public UnitType _unitType;
        
        public Transform ItemsParent;

        public Transform ArmorParent;
        
        public Transform WeaponParent;

        public Transform StatNumbersParent;
        
        private InventorySlot[] _slots;
        
        private EquipmentSlot[] _armorEquipmentSlots;
        
        private EquipmentSlot[] _weaponEquipmentSlots;
        
        public Image ClassImage;
        
        private Sprite[] _classImages = new Sprite[3];
        
        private Interaction.Inventory _inventory;

        private void Start()
        {
            //_unitType = DataManager.Instance.PlayerUnitType;
            
            _classImages[0] = Resources.Load<Sprite>("Sprites/Weapons/weapon_lavish_sword");
            _classImages[1] = Resources.Load<Sprite>("Sprites/Weapons/weapon_bow");
            _classImages[2] = Resources.Load<Sprite>("Sprites/Weapons/weapon_red_magic_staff");

            //ClassImage.sprite = _classImages[(int)_unitType];

            for (int i = 0; i < 8; i++)
            {
                StatNumbersParent.GetChild(i).GetComponent<TMP_Text>().text = DataManager.Instance.Stats[i].ToString();
            }
            
            
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
            GameManager.Instance.ChangeState(GameState.Gameplay);
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