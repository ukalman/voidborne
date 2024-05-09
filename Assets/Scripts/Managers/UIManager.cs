using System;
using Utilities;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        
        public static UIManager Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Debug.Log("UI Manager awake.");
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            // Subscribe to actions/events here
        }

        

        private void OnDestroy()
        {
            // Unsubscribe to actions/events here
          
        }

        
        private void Play()
        {
            Debug.Log("UI Manager Play is called.");
            //CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.MainMenu, 0);
        }
    
        /*
        public void StartGame()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.MainMenu, 0);
        }
        */
        public void OpenMainMenu()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.MainMenu, 0);
        }
        
        public void OpenSettings()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Settings, 0);
        }

        public void OpenCharacterCreationPanel()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.CharacterCreation, 0);
        }

        
        
        
        
    }
}