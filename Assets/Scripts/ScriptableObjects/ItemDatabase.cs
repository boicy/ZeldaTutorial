using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public InventoryItem GetItem(string itemName)
    {
        return items.Where(InventoryItemNameMatches(itemName)).DefaultIfEmpty(null).FirstOrDefault();
    }

    private static Func<InventoryItem, bool> InventoryItemNameMatches(string itemName)
    {
        return item => item.itemName == itemName;
    }

    internal void Reset()
    {
        items.ForEach(item => item.Reset());
    }
}
