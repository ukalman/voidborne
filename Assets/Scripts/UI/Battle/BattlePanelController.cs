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

        public Button AttackButton;
        public Button HealButton;
        

        private void Awake()
        {
            BattleManager.Instance.onBattlePanelCreated?.Invoke(this);
        }

        private void Start()
        {
            SetupBattle();
        }

        private void Update()
        {
            UpdateInteractivity();
            UpdateDialogueText();
        }

        void SetupBattle()
        {
            
            PlayerHUD.SetHUD(BattleManager.Instance.PlayerUnit);
            EnemyHUD.SetHUD(BattleManager.Instance.EnemyUnit);
            
        }

        public void OnAttackButton()
        {
            StartCoroutine(BattleManager.Instance.PlayerAttack());
        }
        
        public void OnHealButton()
        {
            StartCoroutine(BattleManager.Instance.PlayerHeal());
        }

        private void UpdateInteractivity()
        {
            if (BattleManager.Instance.State != BattleState.PLAYERTURN)
            {
                AttackButton.interactable = false;
                HealButton.interactable = false;
            }
            else
            {
                AttackButton.interactable = true;
                HealButton.interactable = true; 
            }
        }

        private void UpdateDialogueText()
        {
            DialogueText.text = BattleManager.Instance.DialogueText;
        }
        
        
    }
}