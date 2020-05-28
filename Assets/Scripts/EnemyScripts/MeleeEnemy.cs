using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{

    protected override bool TargetIsInsideRangeToChaseAndAttack()
    {
        return TargetIsInsideChaseRadius()
                    && TargetIsInsideAttackRadius();
    }

    protected override void DoAttackingBehaviour()
    {
        if (EnemyIsWalkingOrIsStaggered())
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private bool TargetIsInsideAttackRadius() => Vector3.Distance(target.position, transform.position) <= attackRadius;

    private bool TargetIsInsideChaseRadius() => Vector3.Distance(target.position, transform.position) <= chaseRadius;

    private bool EnemyIsWalkingOrIsStaggered() => (currentState == EnemyState.walk) && (currentState != EnemyState.stagger);

    public IEnumerator AttackCoroutine()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }

    protected override bool TargetIsOutsideRangeToChase()
    {
        //don't want to do anything as have no idle state
        return false;
    }

    protected override void DoChasingBehaviour()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position,
                                                    target.position,
                                                    moveSpeed * Time.deltaTime);
        changeAmim(temp - transform.position);
        myRigidBody.MovePosition(temp);
        ChangeState(EnemyState.walk);
    }
}
