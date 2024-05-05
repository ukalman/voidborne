using System;
using Utilities;
using UI;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        
        public static UIManager Instance { get; private set; }
        
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
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.MainMenu, 0);
        }
        
        
    }
}