using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.CharacterCreation
{
    public class CharacterCreationPanelController : MonoBehaviour
    {
        public TMP_InputField CharacterNameInputField;
        public GameObject CharacterStatsPanel;
        public TMP_Text FreePointsDisplay;

        public Button ReadyUnreadyButton;
        public Button ConfirmButton;
        public Button ResetButton;

        private bool _isReady = false;
        
        public static int FreePoints = 10;

        public static void OnDecrementStat()
        {
            FreePoints += 1;
        }
        
        public static void OnIncrementStat()
        {
            FreePoints -= 1;
        }

        private void UpdateFreePointsDisplay()
        {
            FreePointsDisplay.text = FreePoints.ToString();
        }

        private void Update()
        {
            UpdateFreePointsDisplay();
            UpdateButtonInteractivity();
        }

        private bool CheckIfReadyToPressReady()
        {
            if (CharacterNameInputField.text != "" && FreePoints <= 0) return true;
            return false;
        }
        
        public void UpdateButtonInteractivity()
        {
            ReadyUnreadyButton.interactable = CheckIfReadyToPressReady();
            ConfirmButton.interactable = _isReady;
            
        }

        public void ResetAttributes()
        {
            
        }
        
    }
}