using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using Units.Enemies;
using Units.Heroes;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Managers
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance { get; private set; }

        private List<ScriptableUnit> _units;

        public BaseHero SelectedHero;
        
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

            _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
        }

        
        // TODO Make this and SpawnEnemies generic functions
        public void SpawnHeroes()
        {
            var heroCount = 1;
            for (int i = 0; i < heroCount; i++)
            {
                var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
                var spawnedHero = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

                randomSpawnTile.SetUnit(spawnedHero);

            }
            
            GameManager.Instance.ChangeState(GameState.SpawnEnemies);
        }
        
        // TODO Make this and SpawnHeroes generic functions
        public void SpawnEnemies()
        {
            var enemyCount = 1;
            for (int i = 0; i < enemyCount; i++)
            {
                var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
                var spawnedEnemy = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

                randomSpawnTile.SetUnit(spawnedEnemy);

            }
            
            GameManager.Instance.ChangeState(GameState.HeroesTurn);
        }

        private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
        {
            // go through the list (_units), we want all of the units according to the faction that we want, we randomly shuffle them, then select the first one and return its prefab
            return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
        }

        public void SetSelectedHero(BaseHero hero)
        {
            SelectedHero = hero;
            MenuManager.Instance.ShowSelectedHero(hero);
        }
        
    }
}