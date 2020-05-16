using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonInteriorEnemyRoom: DungeonInteriorRoom
{
    public Door[] doors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CheckEnemies()
    {        
        if (!enemies.Any(enemy => enemy.gameObject.activeInHierarchy))
        {
            OpenDoors();
        }           
    }

    public void OpenDoors() => Array.ForEach(doors, door => door.Open());

    public void CloseDoors() => Array.ForEach(doors, door => door.Close());
}
