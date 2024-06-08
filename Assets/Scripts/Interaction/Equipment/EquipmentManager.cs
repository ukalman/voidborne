using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;
using Utilities;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public Equipment[] CurrentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);

    public delegate void OnEquipmentChangedUI();
    
    public OnEquipmentChanged onEquipmentChanged;

    public OnEquipmentChangedUI onEquipmentChangedUI;
    
    private Inventory _inventory;

    private void Start()
    {
        _inventory = Inventory.Instance;
        
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlotEnum)).Length;
        CurrentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipSlot;

        Equipment oldItem = null;
        
        if (CurrentEquipment[slotIndex] != null)
        {
            oldItem = CurrentEquipment[slotIndex];
            _inventory.Add(oldItem);
        }

        onEquipmentChanged?.Invoke(newItem, oldItem);
        
        
        CurrentEquipment[slotIndex] = newItem;
        
    }

    public void Unequip(int slotIndex)
    {
        if (CurrentEquipment[slotIndex] != null)
        {
            Equipment oldItem = CurrentEquipment[slotIndex];
            _inventory.Add(oldItem);

            CurrentEquipment[slotIndex] = null;
            
            onEquipmentChanged?.Invoke(null, oldItem);
            
            onEquipmentChangedUI?.Invoke();
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < CurrentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
