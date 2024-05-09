using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities;

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
        public Button ReturnButton;
        
        
        public TMP_Text ReadyUnreadyText;
        
        public static bool IsReady = false;
        
        public static int FreePoints = 10;
        public static bool IsReadyClicked = false;

        public static UnityAction OnResetAttributes = delegate { };
        
        
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
            ConfirmButton.interactable = IsReady;
            ResetButton.interactable = !IsReady && (FreePoints < 10);

            CharacterNameInputField.interactable = !IsReady;

        }

        public void ResetAttributes()
        {
            if (FreePoints < 10)
            {
                OnResetAttributes?.Invoke();
                FreePoints = 10;
            }
            
        }

        public void OnReadyUnreadyClicked()
        {
            IsReady = !IsReady;

            ReadyUnreadyText.text = IsReady ? "Go Back" : "Ready";
        }

        public void OnReturnButtonClicked()
        {
            GameManager.Instance.ChangeState(GameState.Start);
        }
        
    }
}