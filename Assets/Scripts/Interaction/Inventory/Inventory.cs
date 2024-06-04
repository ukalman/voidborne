using System;
using System.Collections.Generic;
using UnityEngine;


namespace Interaction
{
    public class Inventory : MonoBehaviour
    {
        #region Singleton
        
        public static Inventory Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of Inventory found!");
                return;
            }
            Instance = this;
        }
        
        #endregion

        public delegate void OnItemChanged();

        public OnItemChanged OnItemChangedCallback;
        
        public int Space = 20;
        
        
        public List<Item> Items = new List<Item>();

        public bool Add(Item item)
        {
            if (!item.isDefaultItem)
            {
                if (Items.Count >= Space)
                {
                    Debug.Log("Not enough room.");
                    return false;
                }
                
                Items.Add(item);

                OnItemChangedCallback?.Invoke();
                
                return true;
            }

            return false;

        }

        public void Remove(Item item)
        {
            Items.Remove(item);
            
            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();   
            }
        }
        
    }
}