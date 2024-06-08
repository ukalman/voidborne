using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
    public class ScriptableUnit : ScriptableObject
    {
        [FormerlySerializedAs("HeroType")] public UnitType unitType;
        public Faction Faction;
        public BaseUnit UnitPrefab;
    }
}