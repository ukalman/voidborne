using System;
using System.Collections;
using Interaction;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState GameState;
       
        private void Awake() {
            if (Instance == null) {
                Debug.Log("Game Manager awake.");
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            //ChangeState(GameState.Start);
            ChangeState(GameState.LevelPrep);
        }
        
        /*
        private void Start() {
            StartCoroutine(InitializeGame());
        }

        IEnumerator InitializeGame() {
            while (UIManager.Instance == null)
                yield return null;  // Wait until UIManager is initialized
            ChangeState(GameState.Start);
            //ChangeState(GameState.GenerateGrid);
        }
        */

        public void ChangeState(GameState newState)
        {
            GameState = newState;
            
            switch(newState)
            {
                case GameState.None:
                    break;
                case GameState.Start:
                    Debug.Log("Game State is Start!");
                    Debug.Log(UIManager.Instance);
                    UIManager.Instance.OpenMainMenu();
                    break;
                case GameState.CharacterCreation:
                    UIManager.Instance.OpenCharacterCreationPanel();
                    break;
                case GameState.GenerateGrid:
                    GridManager.Instance.GenerateGrid();
                    break;
                case GameState.Battle:
                    StartCoroutine(BattleManager.Instance.InitiateBattle());
                    ChangeState(GameState.HeroesTurn);
                    break;
                case GameState.LevelPrep:
                    LevelPreparation();
                    break;
                case GameState.Gameplay:
                    break;
                case GameState.SpawnHeroes:
                    UnitManager.Instance.SpawnHeroes();
                    break;
                case GameState.SpawnEnemies:
                    UnitManager.Instance.SpawnEnemies();
                    break;
                case GameState.Inventory:
                    Debug.Log("Inventory Turn");
                    break;
                case GameState.HeroesTurn:
                    Debug.Log("Heroes Turn");
                    break;
                case GameState.EnemiesTurn:
                    Debug.Log("Enemies Turn");
                    break;
                case GameState.LevelEnd:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void LevelPreparation()
        {
            //GridManager.Instance.GenerateGrid();
            //UnitManager.Instance.SpawnHeroes();
            //UnitManager.Instance.SpawnEnemies();
            //InteractableManager.Instance.SpawnItems();
            GridManager.Instance.FillGrid();
            ChangeState(GameState.HeroesTurn);
        }
        
        public void EndTurn()
        {
            if (GameState == GameState.HeroesTurn)
            {
                ChangeState(GameState.EnemiesTurn);
            }
            else if (GameState == GameState.EnemiesTurn)
            {
                ChangeState(GameState.HeroesTurn);
            }
        }
        
        
        
        
    }
    
    
    
}