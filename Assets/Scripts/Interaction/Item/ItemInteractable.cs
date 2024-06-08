using Managers;
using UnityEngine;
using Utilities;

namespace Interaction
{
    public class ItemInteractable : Interactable
    {
        public Item Item;

        public UnitType WhichUnitType;
        
        public float amplitude = 1f;  
        public float frequency = 1f;
        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;  // Store the initial position of the item
        }

        public override void Update()
        {
            base.Update();
            // Calculate the new position using a sine wave
            float newX = startPosition.x + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(newX, startPosition.y, startPosition.z);
        }

        public override void Interact()
        {
            PickUp();
        }

        protected override bool CheckIfInteractable()
        {
            if (UnitManager.Instance.SelectedHero.OccupiedTile == OccupiedTile)
            {
                return true;
            }

            return false;
        }

        void PickUp()
        {
            if (DataManager.Instance.PlayerUnitType == WhichUnitType || WhichUnitType == UnitType.None)
            {
                Debug.Log("Picking up " + Item.ItemName);
                bool wasPickedUp = Inventory.Instance.Add(Item);
                InteractableManager.Instance.RemoveFocus();

                if (wasPickedUp)
                {
                    Destroy(gameObject);
                }
            }
            
            
        }
    }
}