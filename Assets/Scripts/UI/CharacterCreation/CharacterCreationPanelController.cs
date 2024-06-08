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
        public CharacterClassInputPanelController CharacterClassPanel;
        public TMP_Text FreePointsDisplay;

        public Button ReadyUnreadyButton;
        public Button ConfirmButton;
        public Button ResetButton;
        public Button ReturnButton;

        private StatInputPanelController[] _statInputs;
        
        
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

        public void OnConfirmButtonClicked()
        {
            UnitType unitType = UnitType.Knight;
            var unitTypeString = CharacterClassPanel.ClassNameDisplay.text;

            if (unitTypeString.Equals("Archer"))
            {
                unitType = UnitType.Archer;
            } else if (unitTypeString.Equals("Mage"))
            {
                unitType = UnitType.Mage;
            }
            
            
            
            _statInputs = CharacterStatsPanel.GetComponentsInChildren<StatInputPanelController>();
            Debug.Log(_statInputs == null);
            Debug.Log(_statInputs.Length);
            Debug.Log(CharacterNameInputField.text);
            
            Debug.Log("stat total points: ");
            for (int i = 0; i < _statInputs.Length; i++)
            {
                Debug.Log(_statInputs[i].TotalPoints);
            }
            
            DataManager.Instance.CreateAndSaveCharacter(CharacterNameInputField.text, unitType, _statInputs[0].TotalPoints, _statInputs[1].TotalPoints, _statInputs[2].TotalPoints, _statInputs[3].TotalPoints, _statInputs[4].TotalPoints, _statInputs[5].TotalPoints, _statInputs[6].TotalPoints, _statInputs[7].TotalPoints);
            UIManager.Instance.CloseCharacterCreationPanel();
            
            GameManager.Instance.ChangeState(GameState.LevelPrep);
        }
        
    }
}