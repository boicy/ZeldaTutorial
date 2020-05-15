using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    private const string PLAYER_TAG = "Player";

    [Header("Inventory to use")]
    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {
            playerInventory.coins += 1;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
