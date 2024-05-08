using Sirenix.OdinInspector;
using UnityEngine;
using Managers;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuPanelController : MonoBehaviour
    {
        
        public void PlayGame()
        {
            Debug.Log("Play Game button clicked");
            // SceneManager.LoadScene("GameScene");
        }

        public void OpenSettings()
        {
            Debug.Log("Settings button clicked");
            Managers.UIManager.Instance.OpenSettings();
        }

        public void OpenControls()
        {
            Debug.Log("Controls button clicked");
        }

        public void ShowCredits()
        {
            Debug.Log("Credits button clicked");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}