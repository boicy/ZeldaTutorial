using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private const string BREAKABLE_TAG = "breakable";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsBreakable(other))
        {
            other.GetComponent<Pots>().Smash();
        }
    }

    private static bool IsBreakable(Collider2D other)
    {
        return other.CompareTag(BREAKABLE_TAG);
    }
}
