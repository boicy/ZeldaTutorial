using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    [Header("Inventory to use")]
    public Inventory playerInventory;

    [Header("Display Component to use")]
    public TextMeshProUGUI coinDisplay;

    public void UpdateCoinCount()
    {
        coinDisplay.text = "" + playerInventory.coins;
    }

}
