using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CharacterCreation
{
    public class StatInputPanelController : MonoBehaviour
    {

        public string StatName;
    
        public TMP_Text StatPointsDisplay;
        public Button DecrementButton;
        public Button IncrementButton;

        public TMP_Text ExtraPointsDisplay;
    

        public int StatPoints = 0;
        public int ExtraPoints = 0;
        public int TotalPoints = 0;
        private const int MinStatPoints = 0;
        private const int MaxStatPoints = 20;

       
        
    
        // Start is called before the first frame update
        void Start()
        {
            CharacterClassInputPanelController.OnClassChange += OnClassChange;
            CharacterCreationPanelController.OnResetAttributes += OnResetAttribute;
            StatPointsDisplay.text = StatPoints.ToString();
            UpdateButtonInteractivity();
            OnClassChange("Knight");
        }

        private void Update()
        {
            UpdateButtonInteractivity();
            CheckIfReady(); // Also updates Stat Point Display
            UpdateExtraPointsDisplay();
        }

        private void OnDestroy()
        {
            CharacterClassInputPanelController.OnClassChange -= OnClassChange;
            CharacterCreationPanelController.OnResetAttributes -= OnResetAttribute;
        }

        private void OnClassChange(string className)
        {
            switch(className)
            {
                case "Knight":
                    if (StatName is "Strength" or "Armor" or "Charisma")
                    {
                        ExtraPoints = StatName is "Charisma" ? 3 : 5;
                    }

                    else ExtraPoints = 0;
                   
                    break;
                case "Archer":
                    ExtraPoints = StatName is "Dexterity" or "Agility" or "Focus" ? 5 : 0;
                
                    break;
                case "Mage":
                    if (StatName is "Intelligence" or "Power" or "Charisma")
                    {
                        ExtraPoints = StatName is "Charisma" ? 3 : 5;
                    }

                    else ExtraPoints = 0;
                    
                    break;
            }
            //UpdateExtraPointsDisplay();
        }

        private void OnResetAttribute()
        {
            StatPoints = 0;
            TotalPoints = 0;
        }
    

        private void UpdateButtonInteractivity()
        {
            IncrementButton.interactable = (StatPoints < MaxStatPoints) && CharacterCreationPanelController.FreePoints > 0;
            DecrementButton.interactable = StatPoints > MinStatPoints;
        }

        private void CheckIfReady()
        {
            if (CharacterCreationPanelController.IsReady)
            {
                DecrementButton.gameObject.SetActive(false);
                IncrementButton.gameObject.SetActive(false);
                TotalPoints = StatPoints + ExtraPoints;

            }
            else
            {
                DecrementButton.gameObject.SetActive(true);
                IncrementButton.gameObject.SetActive(true);
            }
            UpdateStatPointsDisplay();
        }

        public void IncrementStatPoints()
        {
            if (StatPoints < MaxStatPoints)
            {
                StatPoints++;
                CharacterCreationPanelController.OnIncrementStat();
                //UpdateStatPointsDisplay();
            }
            
            
            
        }
    
        public void DecrementStatPoints()
        {
            if (StatPoints > MinStatPoints)
            {
                StatPoints--;
                CharacterCreationPanelController.OnDecrementStat();
                //UpdateStatPointsDisplay();
            }
            
        }

        private void UpdateStatPointsDisplay()
        {
            StatPointsDisplay.text = !CharacterCreationPanelController.IsReady ? StatPoints.ToString() : TotalPoints.ToString();


            //UpdateButtonInteractivity();
        }

        private void UpdateExtraPointsDisplay()
        {

            if (!CharacterCreationPanelController.IsReady)
            {
                if (ExtraPoints == 0)
                {
                    ExtraPointsDisplay.text = "";
                }
                else
                {
                    ExtraPointsDisplay.text = "+" + ExtraPoints;
                } 
            }
            else
            {
                ExtraPointsDisplay.text = "";
            }
            
            
        }

        private void CalculateTotalPoints()
        {
            TotalPoints = StatPoints + ExtraPoints;
        }
    
    }
}
