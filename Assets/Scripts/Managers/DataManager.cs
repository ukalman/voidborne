using System;
using System.Collections.Generic;
using Data;
using Units.Heroes;
using UnityEngine;
using Utilities;

namespace Managers
{
    /*
     Testing and Validation: Rigorously test saving and loading functionality under various conditions:
     When there is no existing saved data.
     With normal and edge-case hero attributes.
     After modifying the structure of HeroData to see how changes affect loading older saved data formats.
     */
    
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        //public MainData MainData;

        public string AvatarName;
        public BaseHero Hero;
        public UnitType PlayerUnitType;

        public int[] Stats;

        public delegate void OnPlayerCreated();

        public static OnPlayerCreated onPlayerCreated;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Stats = new int[8];
            //throw new NotImplementedException();
            // Initialize hero (if created)
        }

        // Character will be created after the Player has determined the attributes, and the class of the Hero, then will be saved to MainData and PlayerPrefs
        public void CreateAndSaveCharacter(string avatarName, UnitType unitType, int strength, int armor, int power, int intelligence, int dexterity, int agility, int charisma, int focus)
        {
            BaseHero hero = CreateCharacter(unitType, strength, armor, power, intelligence, dexterity, agility,
                charisma, focus);
            SaveCharacter(avatarName, hero);
            onPlayerCreated?.Invoke();
            // SaveCharacterToPrefs(hero); YORUMDAN ÇIKAR
        }

        // Character will be created after the Player has determined the attributes, and the class of the Hero
        public BaseHero CreateCharacter(UnitType unitType, int strength, int armor, int power, int intelligence, int dexterity, int agility, int charisma, int focus)
        {
            PlayerUnitType = unitType;

            Stats[0] = strength;
            Stats[1] = armor;
            Stats[2] = power;
            Stats[3] = intelligence;
            Stats[4] = dexterity;
            Stats[5] = agility;
            Stats[6] = charisma;
            Stats[7] = focus;
                        
        
            return UnitManager.Instance.GetBaseHero(unitType, strength, armor, power, intelligence, dexterity, agility,
                charisma, focus);
        }
        
        // To save the character to MainData (MainData is used during runtime)
        private void SaveCharacter(string avatarName, BaseHero hero)
        {
            AvatarName = avatarName;
            Hero = hero;
        }

        // To load the character to MainData (MainData is used during runtime)
        private void LoadCharacter(HeroData heroData)
        {
            Hero = UnitManager.Instance.GetBaseHero(heroData.unitType, heroData.Strength, heroData.Armor,
                heroData.Power, heroData.Intelligence, heroData.Dexterity, heroData.Agility,
                heroData.Charisma, heroData.Focus);
        }
        
        // To save the character to PlayerPrefs
        private void SaveCharacterToPrefs(BaseHero hero)
        {
            HeroData heroData = new HeroData(hero);
            string heroJson = JsonUtility.ToJson(heroData);
            PlayerPrefs.SetString("HeroData", heroJson);
            PlayerPrefs.Save();
        }

        // To load the character from PlayerPrefs 
        public void LoadCharacterFromPrefs()
        {
            string heroJson = PlayerPrefs.GetString("HeroData", null);
            if (string.IsNullOrEmpty(heroJson))
            {
                // Optionally handle scenario where no hero data is found
                Debug.Log("No hero data found in PlayerPrefs.");
                return;
            }

            HeroData heroData = JsonUtility.FromJson<HeroData>(heroJson);
            if (heroData != null)
            {
                LoadCharacter(heroData);
            }
            else
            {
                // Optionally handle corrupted data
                Debug.LogError("Failed to load hero data from PlayerPrefs.");
            }

        }
        
    }
}