using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> myInventory = new List<InventoryItem>();

    internal void Reset()
    {
        //can reset here to include Sword and delete everything else
        //but just removing everything to start with
        myInventory.Clear();
    }
}