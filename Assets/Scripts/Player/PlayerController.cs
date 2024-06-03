using Units.Heroes;
using Interaction;

namespace Player
{
    public class PlayerController
    {
        public Interactable Focus;
        private BaseHero _heroCharacter;


        void SetFocus(Interactable newFocus)
        {
            Focus = newFocus;
        }

        void RemoveFocus()
        {
            Focus = null;
        }
        
        
    }
}