using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    private const string PLAYER_TAG = "Player";
    private const string OPENED = "opened";

    [Header("What's in the box?")]
    public Item contents;
    public bool isOpen;
    public BoolValue storedOpen;

    [Header("Players Inventory")]
    public Inventory playerInventory;

    [Header("Chest text and stuff")]
    public GameObject dialogBox;
    public Text dialogText;
    public SignalSender raiseItem;

    [Header("Animation")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            anim.SetBool(OPENED, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInterects())
        {
            if (!isOpen)
            {
                // Open the chest
                OpenChest();
            }
            else
            {
                //chest is already open
                ChestIsAlreadyOpen();
            }
        }
    }

    private bool PlayerInterects()
    {
        return Input.GetButtonDown("Weapon Attack") && playerInRange;
    }

    private void OpenChest()
    {
        //dialog window on
        dialogBox.SetActive(true);
        //dialog text = item contents text
        dialogText.text = contents.itemDescription;
        //add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //raise signal
        raiseItem.Raise();
        //raise the context clue
        context.Raise();
        //set chest opened
        isOpen = true;
        anim.SetBool(OPENED, true);
        storedOpen.RuntimeValue = isOpen;
    }
    
    private void ChestIsAlreadyOpen()
    {        
        //dialog off
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerInteracts(other))
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (PlayerInteracts(other))
        {
            context.Raise();
            playerInRange = false;
        }
    }

    private bool PlayerInteracts(Collider2D other)
    {
        return other.CompareTag(PLAYER_TAG) && !other.isTrigger && !isOpen;
    }
}
