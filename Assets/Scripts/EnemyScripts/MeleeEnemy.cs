using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{

    //public virtual void CheckDistance()
    //{
    //    if (TheTargetIsInRangeToChase())
    //    {
    //        if (InAValidStateToChaseTarget())
    //        {
    //            DoChasingBehaviour(); //DoChjasing Behaviour!
    //        }
    //    }
    //    else if (TargetIsInsideRangeToChaseAndAttack())
    //    {
    //        DoAttackingBehaviour();
    //    }
    //    else if (TargetIsOutsideRangeToChase())
    //    {
    //        DoRestingBehaviour();
    //    }
    //}

    protected override bool TargetIsInsideRangeToChaseAndAttack()
    {
        return Vector3.Distance(target.position, transform.position) <= chaseRadius
                    && Vector3.Distance(target.position, transform.position) <= attackRadius;
    }

    protected override void DoAttackingBehaviour()
    {
        StartCoroutine(AttackCoroutine()); 
    }

    public IEnumerator AttackCoroutine()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);
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
