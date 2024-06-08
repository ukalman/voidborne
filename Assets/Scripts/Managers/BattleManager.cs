using System;
using System.Collections;
using System.Linq;
using UI.Battle;
using Units;
using Units.Enemies;
using Units.Heroes;
using UnityEngine;
using Utilities;

namespace Managers
{
    
    public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}
    
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        public BattleState State;
        
        
        public GameObject PlayerPrefab;
        public GameObject EnemyPrefab;
        
        public BaseHero PlayerUnit;
        public BaseEnemy EnemyUnit;

        public string DialogueText;
        
        public BattlePanelController PanelController;
        
        public delegate void OnBattleInitiated(BaseEnemy enemyUnit);
    
        public OnBattleInitiated onBattleInitiated;
        
        public delegate void OnBattlePanelCreated(BattlePanelController controller);
    
        public OnBattlePanelCreated onBattlePanelCreated;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        private void Start()
        {

            DataManager.onPlayerCreated += OnPlayerCreated;
            onBattlePanelCreated += SetPanel;
            onBattleInitiated += InitiateBattle;
        }

        private void OnDestroy()
        {
            DataManager.onPlayerCreated -= OnPlayerCreated;
            onBattlePanelCreated -= SetPanel;
            onBattleInitiated -= InitiateBattle;
        }

        void OnPlayerCreated()
        {
            PlayerUnit = DataManager.Instance.Hero;
            PlayerPrefab = PlayerUnit.gameObject;
        }

        void SetPanel(BattlePanelController controller)
        {
            PanelController = controller;
        }
        
        void InitiateBattle(BaseEnemy enemyUnit)
        {

            StartCoroutine(SetupBattle(enemyUnit));

        }

        public IEnumerator SetupBattle(BaseEnemy enemyUnit)
        {
            State = BattleState.START;
            
            Debug.Log("Battle started!");
            
            MenuManager.Instance.DeactivateObjects();
            
            EnemyUnit = enemyUnit;
            
            EnemyPrefab = enemyUnit.gameObject;

            DialogueText = "The battle starts... Get Ready!";
            
            
            yield return new WaitForSeconds(2f);
            
            State = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        

        void PlayerTurn()
        {
            DialogueText = "Choose an action!";
;        }

        public void OnAttackButton()
        {
            if (State != BattleState.PLAYERTURN)
            {
                return;
            }

            StartCoroutine(PlayerAttack());
        }
        
        public void OnHealButton()
        {
            if (State != BattleState.PLAYERTURN)
            {
                return;
            }

            StartCoroutine(PlayerHeal());
        }

        public IEnumerator PlayerAttack()
        {
            int damage = PlayerUnit.Attack(EnemyUnit);

            PanelController.EnemyHUD.SetHP(EnemyUnit.CurrentHealth);
            DialogueText = EnemyUnit.UnitName + " got " + damage.ToString() + " damage!";
            
            
            bool isDead = EnemyUnit.DeathControl();
            
            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                State = BattleState.WON;
                EndBattle();
            }
            else
            {
                State = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }

        }

        public IEnumerator PlayerHeal()
        {
            PlayerUnit.Heal(10);
            
            PanelController.PlayerHUD.SetHP(PlayerUnit.CurrentHealth);
            DialogueText = "You are healed!";

            yield return new WaitForSeconds(1f);

            State = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        IEnumerator EnemyTurn()
        {
            DialogueText = EnemyUnit.UnitName + " attacks!";

            yield return new WaitForSeconds(1f);

            EnemyUnit.Attack(PlayerUnit);
            
            bool isDead = PlayerUnit.DeathControl();
            
            PanelController.PlayerHUD.SetHP(PlayerUnit.CurrentHealth);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                State = BattleState.LOST;
                EndBattle();
            }
            else
            {
                State = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            
            
        }

        void EndBattle()
        {
            if (State == BattleState.WON)
            {
                DialogueText = "Battle is Won!";
            } else if (State == BattleState.LOST)
            {
                DialogueText = "You are defeated! Game Over!";
            }
        }
    }
}