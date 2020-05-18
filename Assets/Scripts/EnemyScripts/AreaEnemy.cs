using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D boundary;

    public override bool TheTargetIsInRange()
    {
        return Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius
            && boundary.bounds.Contains(target.transform.position);
    }

    public override bool TargetIsOutsideRange()
    {
        return Vector3.Distance(target.position, transform.position) > chaseRadius
            || !boundary.bounds.Contains(target.transform.position);
    }
}
