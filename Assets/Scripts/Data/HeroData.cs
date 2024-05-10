using System;
using Units.Heroes;
using Utilities;

namespace Data
{
    [Serializable]
    public class HeroData
    {
        public HeroType HeroType; // Assuming HeroType is serializable
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
                // HeroType = hero.HeroType;  // Assuming HeroType is a field you want to serialize
                Strength = hero.Strength;
                Armor = hero.Armor;
                Power = hero.Power;
                Intelligence = hero.Intelligence;
                Dexterity = hero.Dexterity;
                Agility = hero.Agility;
                Charisma = hero.Charisma;
                Focus = hero.Focus;
            }
        }

        // Empty constructor for deserialization
        public HeroData() { }
        
    }
    
    
}