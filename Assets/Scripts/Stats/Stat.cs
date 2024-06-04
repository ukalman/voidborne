using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField]
        private int baseValue;

        private List<int> _modifiers = new List<int>();
        
        public int GetValue()
        {
            int finalValue = baseValue;
            _modifiers.ForEach(t => finalValue += t);
            
            return finalValue;
        }

        public void SetValue(int value)
        {
            baseValue = value;
        }
        
        public void AddModifier(int modifier)
        {
            if (modifier != 0)
            {
                _modifiers.Add(modifier);
            }
        }

        public void RemoveModifier(int modifier)
        {
            if (modifier != 0)
            {
                _modifiers.Remove(modifier);
            }
        }
        

        

    }
}