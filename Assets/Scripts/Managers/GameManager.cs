using System;
using System.Collections;
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
            ChangeState(GameState.Start);
            //ChangeState(GameState.GenerateGrid);
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
                case GameState.GenerateGrid:
                    GridManager.Instance.GenerateGrid();
                    break;
                case GameState.LevelPrep:
                    break;
                case GameState.Gameplay:
                    break;
                case GameState.SpawnHeroes:
                    UnitManager.Instance.SpawnHeroes();
                    break;
                case GameState.SpawnEnemies:
                    UnitManager.Instance.SpawnEnemies();
                    break;
                case GameState.HeroesTurn:
                    break;
                case GameState.EnemiesTurn:
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
        
        
        
    }
    
    
    
}