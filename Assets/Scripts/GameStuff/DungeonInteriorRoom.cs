using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInteriorRoom : Room
{

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);     
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}
