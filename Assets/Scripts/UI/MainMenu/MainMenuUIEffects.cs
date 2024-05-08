using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace UI.MainMenu
{
    public class MainMenuUIEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // Default Text Color is: A18D8D
        public string defaultColor = "#A18D8D";
        public string hoverColor = "#5C3F3F"; // Set this to the darker color you want

        private TMP_Text buttonText;

        void Start()
        {
            buttonText = GetComponentInChildren<TMP_Text>();
            buttonText.color = HexToColor(defaultColor); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonText.color = HexToColor(hoverColor);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonText.color = HexToColor(defaultColor);
        }
        
        Color HexToColor(string hex)
        {
            if (!hex.StartsWith("#"))
                hex = "#" + hex;
          
            if (hex.Length == 7)
                hex += "FF"; // Add full alpha

            if (ColorUtility.TryParseHtmlString(hex, out Color color))
                return color;
           
              
            return Color.white;
           
                
        }
    }
}