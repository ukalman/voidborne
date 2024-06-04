using UnityEngine;

namespace Interaction
{
    public class ItemPickup : Interactable
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

        private void Update()
        {
            // Calculate the new position using a sine wave
            float newX = startPosition.x + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(newX, startPosition.y, startPosition.z);
        }

        public override void Interact()
        {
            PickUp();
        }

        void PickUp()
        {
            Debug.Log("Picking up " + Item.ItemName);
            bool wasPickedUp = Inventory.Instance.Add(Item);

            if (wasPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}