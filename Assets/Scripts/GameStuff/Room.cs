using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public Enemy[] enemies;
    public Pots[] pots;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {
            //activate all enemies and pots
            Array.ForEach(enemies, new Action<Enemy>(Activate));
            Array.ForEach(pots, new Action<Pots>(Activate));
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger) {
            //Deactivate all enemies and pots
            Array.ForEach(enemies, new Action<Enemy>(Deactivate));
            Array.ForEach(pots, new Action<Pots>(Deactivate));
        }
    }

    private void Activate(MonoBehaviour component) => component.gameObject.SetActive(true);
    private void Deactivate(MonoBehaviour component) => component.gameObject.SetActive(false);
}
