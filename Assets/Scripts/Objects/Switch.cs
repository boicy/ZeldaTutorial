﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;
   
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.RuntimeValue;
        if (active)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        active = true;
        storedValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            ActivateSwitch();
        }
    }
}
