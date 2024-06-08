using System;
using Managers;
using TMPro;
using Units;
using Units.Enemies;
using Units.Heroes;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class BattlePanelController : MonoBehaviour
    {
        
        public BattleHUD PlayerHUD;
        public BattleHUD EnemyHUD;

        public TMP_Text DialogueText;
        

        private void Awake()
        {
            BattleManager.Instance.onBattlePanelCreated?.Invoke(this);
        }

        private void Start()
        {
            SetupBattle();
        }

        void SetupBattle()
        {
            
            PlayerHUD.SetHUD(BattleManager.Instance.PlayerUnit);
            EnemyHUD.SetHUD(BattleManager.Instance.EnemyUnit);
            
            
            

        }
    }
}