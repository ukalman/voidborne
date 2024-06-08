using System.Collections;
using System.Linq;
using Units;
using Units.Enemies;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        public BaseUnit Player;
        public BaseUnit Enemy;

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

        public IEnumerator InitiateBattle()
        {
            Debug.Log("Battle!");

            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseUnit>();
            Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseUnit>();

            GameManager.Instance.ChangeState(GameState.HeroesTurn);
            yield return null;
        }

        public IEnumerator PlayerTurn()
        {
            Debug.Log("Player's Turn");
            Player.Attack(Enemy);
            yield return new WaitForSeconds(3);
            GameManager.Instance.EndTurn();
        }

        public IEnumerator EnemyTurn()
        {
            Debug.Log("Enemy's Turn");
            Enemy.Attack(Player);
            yield return new WaitForSeconds(3);
            GameManager.Instance.EndTurn();
        }
    }
}