using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable {
    private const string PLAYER_TAG = "Player";

    [Header("Sign details")]
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;   

    // Update is called once per frame
    public virtual void Update ()
    {
        if (PlayerInteracted())
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    private bool PlayerInteracted()
    {
        return Input.GetButtonDown("First Weapon") && playerInRange;
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (PlayerLeaves(other))
        {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

    private static bool PlayerLeaves(Collider2D other)
    {
        return other.CompareTag(PLAYER_TAG) && !other.isTrigger;
    }
}