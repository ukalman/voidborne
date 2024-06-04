using Units.Heroes;
using Interaction;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Interactable Focus;
        private BaseHero _heroCharacter;


        void SetFocus(Interactable newFocus)
        {
            if (newFocus != Focus)
            {
                if (Focus != null)
                {
                    Focus.OnDeFocused(); 
                }
                
                Focus = newFocus;
            }
            
            
            newFocus.OnFocused();
        }

        void RemoveFocus()
        {
            if (Focus != null)
                Focus.OnDeFocused();
            Focus = null;
        }
        
        
    }
}