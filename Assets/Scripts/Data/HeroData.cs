using System;
using Units.Heroes;
using UnityEngine.Serialization;
using Utilities;

namespace Data
{
    [Serializable]
    public class HeroData
    {
        [FormerlySerializedAs("HeroType")] public UnitType unitType; // Assuming UnitType is serializable
        public int Strength;
        public int Armor;
        public int Power;
        public int Intelligence;
        public int Dexterity;
        public int Agility;
        public int Charisma;
        public int Focus;
        
        public HeroData(BaseHero hero)
        {
            if (hero != null)
            {
                // UnitType = hero.UnitType;  // Assuming UnitType is a field you want to serialize
                Strength = hero.Strength.GetValue();
                Armor = hero.Armor.GetValue();
                Power = hero.Power.GetValue();
                Intelligence = hero.Intelligence.GetValue();
                Dexterity = hero.Dexterity.GetValue();
                Agility = hero.Agility.GetValue();
                Charisma = hero.Charisma.GetValue();
                Focus = hero.Focus.GetValue();
            }
        }

        // Empty constructor for deserialization
        public HeroData() { }
        
    }
    
    
}