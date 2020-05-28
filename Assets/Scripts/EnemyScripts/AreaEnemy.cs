using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D boundary;

    protected override bool TheTargetIsInRangeToChase()
    {
        return WithinChaseRadius()
            && OutsideAttackRadius()
            && WithinBoundary();
    }

    private bool WithinBoundary() => boundary.bounds.Contains(target.transform.position);

    private bool OutsideAttackRadius() => Vector3.Distance(target.position, transform.position) > attackRadius;

    private bool WithinChaseRadius() => Vector3.Distance(target.position, transform.position) <= chaseRadius;

    protected override bool TargetIsOutsideRangeToChase()
    {
        return OutsideChaseRadius() || NotWithinBoundary();
    }

    private bool NotWithinBoundary() => !boundary.bounds.Contains(target.transform.position);

    private bool OutsideChaseRadius() => Vector3.Distance(target.position, transform.position) > chaseRadius;
}
