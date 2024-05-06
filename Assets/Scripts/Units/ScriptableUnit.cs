using UnityEngine;
using Utilities;

namespace Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
    public class ScriptableUnit : ScriptableObject
    {
        public Faction Faction;
        public BaseUnit UnitPrefab;
    }
}