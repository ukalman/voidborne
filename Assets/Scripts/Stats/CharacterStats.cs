using System;
using UnityEngine;

namespace Stats
{
    public class CharacterStats : MonoBehaviour
    {
        public int  MaxHealth = 100; // Total vitality
        public int CurrentHealth { get; private set; }
        
        public Stat Strength; //  Directly impacts their melee damage output.
        public Stat Armor; //  Essential for withstanding attacks in melee combat.
        public Stat Power; //  Increases spell damage and critical spell effects.
        public Stat Intelligence;
        public Stat Dexterity; //  Improves precision, critical strikes, and lockpicking abilities.
        public Stat Agility; // Increases dodging capabilities, essential for a character who might wear lighter armor.
        public Stat Charisma; // Useful for manipulating NPCs or bartering with traders using magical artifacts.
        public Stat Focus; // Enhances critical rate and aiming proficiency, crucial for a ranged fighter.

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            Debug.Log(transform.name + " takes " + damage + " damage.");

            if (CurrentHealth <= 0)
            {
                //Die();
            }
        }
        
    }
}