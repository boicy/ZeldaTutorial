using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public Enemy[] enemies;
    public Pots[] pots;
    public GameObject virtualCamera;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {
            PlayerOnEnterTriggerActions(other);
        }
    }

    public virtual void PlayerOnEnterTriggerActions(Collider2D other)
    {
        //activate all enemies and pots
        Array.ForEach(enemies, enemy => enemy.gameObject.SetActive(true));
        Array.ForEach(pots, pot => pot.gameObject.SetActive(true));
        virtualCamera.SetActive(true);
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {
            PlayerOnExitTriggerActions(other);
        }
    }

    public virtual void PlayerOnExitTriggerActions(Collider2D other)
    {
        //Deactivate all enemies and pots
        Array.ForEach(enemies, enemy => enemy.gameObject.SetActive(false));
        Array.ForEach(pots, pot => pot.gameObject.SetActive(false));
        virtualCamera.SetActive(false);
    }

    public void OnDisable()
    {
        virtualCamera.SetActive(false);
    }
}
