using Managers;
using UnityEngine;

namespace Interaction
{
    public class ItemInteractable : Interactable
    {
        public Item Item;

        // Sine wave movement parameters
        public float amplitude = 1f;  // Amplitude of the sine wave
        public float frequency = 1f;  // Frequency of the sine wave
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