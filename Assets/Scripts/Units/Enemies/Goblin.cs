using System;

namespace Units.Enemies
{
    public class Goblin : BaseEnemy
    {
        private void Awake()
        {
            AlertDistance = 3;
        }
    }
}