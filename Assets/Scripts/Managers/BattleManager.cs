using System.Collections;
using System.Linq;
using Units;
using Units.Enemies;
using UnityEngine;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        // Attributes
        // selected hero
        public BaseUnit Player;
        
        // enemy (or enemies)
        public BaseUnit Enemy;
        
        // isBattleOver
        
        
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
            // battle logic vs vs.
            // calls hero's and enemy's Attack functions respectively
            Debug.Log("Battle!");
            
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseUnit>();
            Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseUnit>();

            while (Player != null && Enemy != null)
            {
                yield return StartCoroutine(PlayerTurn());
                yield return StartCoroutine(EnemyTurn());
            }
            
            yield return null;
        }

        public IEnumerator PlayerTurn()
        {
            Player.Attack(Enemy);
            yield return new WaitForSeconds(1);
        }
        
        public IEnumerator EnemyTurn()
        {
            Enemy.Attack(Player);
            yield return new WaitForSeconds(1);
        }
    }
}