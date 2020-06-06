using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ResetableScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;
    public float maxMagic = 10;
    public float currentMagic;


    public void OnEnable()
    {
        currentMagic = maxMagic;
    }

    public void ReduceMagic(float magicCost)
    {
        currentMagic -= magicCost;
    }

    public void AddItem(Item itemToAdd)
    {
        //Is the item a key?
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

    public bool Contains(Item item)
    {
        return items.Contains(item);        
    }

    public override void Reset()
    {
        //no op
    }
}
