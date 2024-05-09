using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.CharacterCreation
{
    public class CharacterClassInputPanelController : MonoBehaviour
    {
        public TMP_Text ClassNameDisplay;
        public TMP_Text ClassInfoDisplay;
        public Image EquipmentImage;
        public Button PreviousButton;
        public Button NextButton;

        private int _currentClassIndex = 0;
        private readonly string[] _classNames = new string[] { "Knight", "Archer", "Mage" };
        private readonly string[] _classDescriptions = new string[] {
            "Knights have high strength, armor, and charisma, making them durable and influential on the battlefield.",
            "Archers are skilled with high dexterity, agility, and focus, capable of attacking multiple enemies at once with precision.",
            "Mages possess high intelligence and power, enabling critical spell damage. They have moderate charisma and can cast spells affecting multiple enemies."
        };
    
        private Sprite[] _equipmentImages = new Sprite[3];

        public static UnityAction<string> OnClassChange = delegate { }; 
    

        void Start()
        {
            _equipmentImages[0] = Resources.Load<Sprite>("Sprites/Weapons/weapon_lavish_sword");
            _equipmentImages[1] = Resources.Load<Sprite>("Sprites/Weapons/weapon_bow");
            _equipmentImages[2] = Resources.Load<Sprite>("Sprites/Weapons/weapon_red_magic_staff");
        
            //UpdateClassDisplay();
            
        }

        private void Update()
        {
            UpdateClassDisplay();
        }


        public void NextClass()
        {
            if (_currentClassIndex < _classNames.Length - 1)
                _currentClassIndex++;
            else
                _currentClassIndex = 0;
            
            OnClassChange?.Invoke(_classNames[_currentClassIndex]);
            
            //UpdateClassDisplay();
        }

        public void PreviousClass()
        {
            if (_currentClassIndex > 0)
                _currentClassIndex--;
            else
                _currentClassIndex = _classNames.Length - 1;
            
            OnClassChange?.Invoke(_classNames[_currentClassIndex]);
            
            //UpdateClassDisplay();
        }

        private void UpdateClassDisplay()
        {
            PreviousButton.gameObject.SetActive(!CharacterCreationPanelController.IsReady);
            NextButton.gameObject.SetActive(!CharacterCreationPanelController.IsReady);
            
            ClassNameDisplay.text = _classNames[_currentClassIndex];
            ClassInfoDisplay.text = _classDescriptions[_currentClassIndex];
            EquipmentImage.sprite = _equipmentImages[_currentClassIndex];  // Update the equipment image
        }
    }
}
