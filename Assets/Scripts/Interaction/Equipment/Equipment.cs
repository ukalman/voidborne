using UnityEngine;
using Utilities;

namespace Interaction
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
    public class Equipment : Item
    {
        public EquipmentSlot EquipSlot;
        
        public int ArmorModifier;
        public int DamageModifier;

        public override void Use()
        {
            base.Use();
            EquipmentManager.Instance.Equip(this);
            // Equip the item
            RemoveFromInventory();
        }
        
        
    }
    
    
    
    
}