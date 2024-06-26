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
        
        public List<BaseEnemy> _enemies;

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
            /*
            var heroCount = 1;
            for (int i = 0; i < heroCount; i++)
            {
                var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
                var spawnedHero = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

                randomSpawnTile.SetUnit(spawnedHero);

            }
            */

            
            /*
            var heroPrefab = GetBaseHero(UnitType.Knight, 10, 10, 10, 10, 10, 10, 10, 10);
            var spawnedHero = Instantiate(heroPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
            
            randomSpawnTile.SetUnit(spawnedHero);
            */
            
        
            
            var spawnedHero = Instantiate(DataManager.Instance.Hero);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
            
            randomSpawnTile.SetUnit(spawnedHero);


            //GameManager.Instance.ChangeState(GameState.SpawnEnemies);
        }
        
        // TODO Make this and SpawnHeroes generic functions
        public void SpawnEnemies()
        {
            var enemyCount = 6;
            /*
            for (int i = 0; i < enemyCount; i++)
            {
                var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
                var spawnedEnemy = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

                randomSpawnTile.SetUnit(spawnedEnemy);

            }
            */

            var goblinPrefab = GetBaseEnemy(UnitType.Goblin);
            var spawnedGoblin = Instantiate(goblinPrefab);
            var goblinSpawnTile = GridManager.Instance.GetEnemySpawnTile();
            
            goblinSpawnTile.SetUnit(spawnedGoblin);
            
            var demonPrefab = GetBaseEnemy(UnitType.Demon);
            var spawnedDemon = Instantiate(demonPrefab);
            var demonSpawnTile = GridManager.Instance.GetEnemySpawnTile();
            
            demonSpawnTile.SetUnit(spawnedDemon);
            
            var undeadPrefab = GetBaseEnemy(UnitType.Undead);
            var spawnedUndead = Instantiate(undeadPrefab);
            var undeadSpawnTile = GridManager.Instance.GetEnemySpawnTile();
            
            undeadSpawnTile.SetUnit(spawnedUndead);
            
            
            _enemies = GameObject.FindGameObjectsWithTag("Enemy")
                .Select(obj => obj.GetComponent<BaseEnemy>())
                .Where(enemy => enemy != null)
                .ToList();
            
            Debug.Log("ENEMIES");
            Debug.Log("First enemy: " + _enemies[0].UnitName);
            Debug.Log("Second enemy: " + _enemies[1].UnitName);
            Debug.Log("Third enemy: " + _enemies[2].UnitName);
            
            
            //GameManager.Instance.ChangeState(GameState.HeroesTurn);
        }

        private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
        {
            // go through the list (_units), we want all of the units according to the faction that we want, we randomly shuffle them, then select the first one and return its prefab
            return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
        }
        
        public BaseHero GetBaseHero(UnitType unitType, int strength, int armor, int power, int intelligence, int dexterity, int agility, int charisma, int focus)
        {
            
            BaseHero hero = (BaseHero)_units.First(u => u.unitType == unitType).UnitPrefab;
            hero.SetAttributes(strength, armor, power, intelligence, dexterity, agility, charisma, focus);
            hero.SetHealth();
            return hero;
        }
        
        public BaseEnemy GetBaseEnemy(UnitType unitType)
        {

            BaseEnemy enemy = (BaseEnemy)_units.First(u => u.unitType == unitType).UnitPrefab;
            enemy.SetHealth();
            return enemy;
        }
        
        

        public void SetSelectedHero(BaseHero hero)
        {
            SelectedHero = hero;
            MenuManager.Instance.ShowSelectedHero(hero);
        }
        
    }
}