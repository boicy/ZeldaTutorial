using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonInteriorEnemyRoom: DungeonInteriorRoom
{
    public Door[] doors;

    public void CheckEnemies()
    {        
        if (!enemies.Any(enemy => enemy.gameObject.activeInHierarchy))
        {
            OpenDoors();
        }           
    }

    public void OpenDoors() => Array.ForEach(doors, door => door.Open());

    public void CloseDoors() => Array.ForEach(doors, door => door.Close());

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        CloseDoors();
    }
}
