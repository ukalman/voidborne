using System.Collections;
using UnityEngine;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        // Attributes
        // selected hero
        
        // enemy (or enemies)
        
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
            yield return null;
        }
    }
}