using System;

namespace Units.Enemies
{
    public class Goblin : BaseEnemy
    {
        private void Awake()
        {
            AlertDistance = 5;
            Health = 5;
            Strength = 1;
            Armor = 1;
        }
    }
}