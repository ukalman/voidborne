using System;
using Tiles;
using UnityEngine;

namespace Interaction
{
    public abstract class Interactable : MonoBehaviour
    {
        public string Name;
        public Tile OccupiedTile;

        private bool _isFocus = false;

        private bool _hasInteracted = false;
        
        public virtual void Update()
        {
            if (_isFocus && !_hasInteracted)
            {
                if (CheckIfInteractable())
                {
                    Interact();
                    _hasInteracted = true;
                }
            }
        }

        public virtual void Interact()
        {
            Debug.Log("Player is interacting with " + Name);
        }
        
        protected virtual bool CheckIfInteractable()
        {
            return false;
        }

       
        
        public void OnFocused()
        {
            _isFocus = true;
            _hasInteracted = false;
        }

        public void OnDeFocused()
        {
            _isFocus = false;
            _hasInteracted = false;
        }
        
        
    }
}