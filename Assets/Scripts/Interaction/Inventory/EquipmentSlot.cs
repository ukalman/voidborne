using UnityEngine;
using UnityEngine.UI;

namespace Interaction
{
    public class EquipmentSlot : MonoBehaviour
    {
        public Image Icon;

        public int SlotIndex;
        //public Button removeButton;
        
        [SerializeField] private Image _placeholderIcon;
        
        private Equipment _equipment;

        public void AddEquipment(Equipment newEquipment)
        {
            _equipment = newEquipment;
            Debug.Log("New Equipment's name: " + newEquipment.name);
            
            Icon.sprite = _equipment.icon;
            Icon.enabled = true;
            //removeButton.interactable = true;
        }

        public void ClearSlot()
        {
            Debug.Log("hello there, slot index: " + SlotIndex);
            _equipment = null;
            Icon.enabled = false;
            //Icon.sprite = _placeholderIcon.sprite;

        }
     
        public void UseItem()
        {
            if (_equipment != null)
            {
                EquipmentManager.Instance.Unequip(SlotIndex);
            }
        }
        
    }
}