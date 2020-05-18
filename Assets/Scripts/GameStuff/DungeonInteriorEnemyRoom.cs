using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonInteriorEnemyRoom: DungeonInteriorRoom
{
    public Door[] doors;

    public void Awake() {
        OpenDoors();
    }

    public void CheckEnemies()
    {        
        if (!enemies.Any(enemy => enemy.gameObject.activeInHierarchy))
        {
            OpenDoors();
        }           
    }

    public override void PlayerOnEnterTriggerActions(Collider2D other)
    {
        AwefulHackToAvoidPolygonCollionTriggeringDoorEarly(other);
        base.PlayerOnEnterTriggerActions(other);
        CloseDoors();
    }

    public override void PlayerOnExitTriggerActions(Collider2D other)
    {
        base.PlayerOnExitTriggerActions(other);
    }

    public void OpenDoors() => Array.ForEach(doors, door => door.Open());

    public void CloseDoors() => Array.ForEach(doors, door => door.Close());

    private static void AwefulHackToAvoidPolygonCollionTriggeringDoorEarly(Collider2D other)
    {
        other.transform.position += new Vector3(0, 1, 0);
    }
}
