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

    public void OpenDoors() => Array.ForEach(doors, door => door.Open());

    public void CloseDoors() => Array.ForEach(doors, door => door.Close()); 

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {
            //activate all enemies and pots
            Array.ForEach(enemies, enemy => enemy.gameObject.SetActive(true));
            Array.ForEach(pots, pot => pot.gameObject.SetActive(true));
            AwefulHackToAvoidPolygonCollionTriggeringDoorEarly(other);
            virtualCamera.SetActive(true);
            CloseDoors();
        }
    }

    private static void AwefulHackToAvoidPolygonCollionTriggeringDoorEarly(Collider2D other)
    {
        other.transform.position += new Vector3(0, 1, 0);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {
            //Deactivate all enemies and pots
            Array.ForEach(enemies, enemy => enemy.gameObject.SetActive(false));
            Array.ForEach(pots, pot => pot.gameObject.SetActive(false));
            virtualCamera.SetActive(false);
        }
    }
}
