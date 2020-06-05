using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InventoryManager : MonoBehaviour
{

    [Header("Inventory Info")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItem currentItem;

    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        useButton.SetActive(buttonActive);
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            playerInventory.myInventory.ForEach(InstantiateItemInSlot);
        }
    }

    private void InstantiateItemInSlot(InventoryItem item)
    {
        if (item.numberHeld > 0 || item.itemName == "Bottle")
        {
            GameObject temp = Instantiate(
                blankInventorySlot,
                inventoryPanel.transform.position,
                Quaternion.identity
                );
            temp.transform.SetParent(inventoryPanel.transform);
            InventorySlot newSlot = temp.GetComponent<InventorySlot>();
            if (newSlot)
            {
                newSlot.Setup(item, this);
            }
        }

    }
    
    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescription, bool isItemUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescription;
        useButton.SetActive(isItemUsable);
    }

    void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UseButtonPressed()
    {
        if (currentItem)
        {
            //use item
            currentItem.Use();
            //clear all inv slots
            ClearInventorySlots();
            //refill all with new numbers
            MakeInventorySlots();
            if (currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
            }
        }
    }
}
