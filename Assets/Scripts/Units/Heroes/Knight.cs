using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Heroes
{
    public class Knight : BaseHero
    {
        private static readonly int Moving = Animator.StringToHash("IsMoving");

        //public Animator animator;
        /*
         * Health: High — Knights are frontline fighters and need substantial health.
         * Armor: High — Essential for withstanding attacks in melee combat.
         * Strength: High — Directly impacts their melee damage output.
         */
        
        /*
         Knight: Intelligence could allow Knights to identify weak points in enemy armor, 
         increasing the damage of their attacks against heavily armored foes. 
         It might also let them strategize better in shield formations or defensive stances, 
         providing bonuses when defending or countering attacks.
         */

        
        // private void Update()
        // {
        //     
        //     AnimationCheck();
        // }
        //
        //
        // private void AnimationCheck()
        // {
        //     animator.SetBool(Moving, IsMoving);
        //    
        // }
        

        public override void SetAttributes(int strength, int armor, int power, int intelligence, int dexterity, int agility, int charisma,
            int focus)
        {
            base.SetAttributes(strength, armor, power, intelligence, dexterity, agility, charisma, focus);
            Debug.Log("Knight attributes set!");
        }
    }
}