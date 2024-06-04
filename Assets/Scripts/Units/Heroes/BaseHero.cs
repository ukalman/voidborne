using System;
using Interaction;

namespace Units.Heroes
{
    public class BaseHero : BaseUnit
    {
        private void Start()
        {
            EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
        }

        void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
        {
            if (newItem != null)
            {
                Armor.AddModifier(newItem.ArmorModifier);
                Strength.AddModifier(newItem.DamageModifier);
                Power.AddModifier(newItem.PowerModifier);
                Intelligence.AddModifier(newItem.IntelligenceModifier);
                Dexterity.AddModifier(newItem.DexterityModifier);
                Agility.AddModifier(newItem.AgilityModifier);
                Charisma.AddModifier(newItem.CharismaModifier);
                Focus.AddModifier(newItem.FocusModifier);
            }
            
            if (oldItem != null)
            {
                Armor.RemoveModifier(oldItem.ArmorModifier);
                Strength.RemoveModifier(oldItem.DamageModifier);
                Power.RemoveModifier(oldItem.PowerModifier);
                Intelligence.RemoveModifier(oldItem.IntelligenceModifier);
                Dexterity.RemoveModifier(oldItem.DexterityModifier);
                Agility.RemoveModifier(oldItem.AgilityModifier);
                Charisma.RemoveModifier(oldItem.CharismaModifier);
                Focus.RemoveModifier(oldItem.FocusModifier);
            }
            
        }
        
    }
}