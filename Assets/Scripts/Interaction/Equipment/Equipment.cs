using UnityEngine;
using Utilities;

namespace Interaction
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
    public class Equipment : Item
    {
        public EquipmentSlot EquipSlot;
        
        public int ArmorModifier; //  Essential for withstanding attacks in melee combat.
        public int DamageModifier; //  Directly impacts the damage output.
        public int PowerModifier; //  Increases spell damage and critical spell effects.
        public int IntelligenceModifier;
        public int DexterityModifier; //  Improves precision, critical strikes, and lockpicking abilities.
        public int AgilityModifier; // Increases dodging capabilities, essential for a character who might wear lighter armor.
        public int CharismaModifier; // Useful for manipulating NPCs or bartering with traders using magical artifacts.
        public int FocusModifier;  // Enhances critical rate and aiming proficiency, crucial for a ranged fighter.
        
        
        public override void Use()
        {
            base.Use();
            EquipmentManager.Instance.Equip(this);
            // Equip the item
            RemoveFromInventory();
        }
        
        
    }
    
    
    
    
}