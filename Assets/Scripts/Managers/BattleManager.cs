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
            Player.Attack(Enemy);
            yield return new WaitForSeconds(1);
            GameManager.Instance.EndTurn();
        }

        public IEnumerator EnemyTurn()
        {
            Enemy.Attack(Player);
            yield return new WaitForSeconds(1);
            GameManager.Instance.EndTurn();
        }
    }
}