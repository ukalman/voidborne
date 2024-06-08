using Managers;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Battle
{
    public class BattleHUD : MonoBehaviour
    {
        public TMP_Text NameText;

        public Slider HpSlider;
        
        public void SetHUD(BaseUnit unit)
        {
            if (unit.Faction == Faction.Hero)
            {
                NameText.text = DataManager.Instance.AvatarName;
            }
            else
            {
                NameText.text = unit.UnitName; 
            }

            HpSlider.maxValue = unit.MaxHealth;
            HpSlider.value = unit.CurrentHealth;

        }

     
        public void SetHP(int hp)
        {
            HpSlider.value = hp;
        }

    }
}