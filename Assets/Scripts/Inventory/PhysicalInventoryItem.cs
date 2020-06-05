using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory();
            Destroy(gameObject);
        }
    }

    void AddItemToInventory()
    {
        if (playerInventory && item)
        {
            if (!playerInventory.myInventory.Contains(item))
            {
                playerInventory.myInventory.Add(item);
            }            
            item.numberHeld += 1;
        }
    }
}
