using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    //public override void CheckDistance()
    //{
    //    if (Vector3.Distance(target.position, transform.position) <= chaseRadius
    //        && Vector3.Distance(target.position, transform.position) > attackRadius)
    //    {
    //        if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
    //        {
    //            if (canFire)
    //            {
    //                Vector3 launchVector = target.transform.position - transform.position;
    //                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
    //                current.GetComponent<Projectile>().Launch(launchVector);
    //                canFire = false;
    //                ChangeState(EnemyState.walk);
    //                anim.SetBool(WAKE_UP, true);
    //            }
    //        }
    //    }
    //    else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
    //    {
    //        anim.SetBool(WAKE_UP, false);
    //    }
    //}

    //public override bool TheTargetIsInRange()
    //{
    //    return base.TheTargetIsInRange();
    //}

    //public override bool InAValidStateToChaseTarget()
    //{
    //    return base.InAValidStateToChaseTarget();
    //}

    public override void DoRangeActionOnTarget()
    {
        if (canFire)
        {
            Vector3 launchVector = target.transform.position - transform.position;
            GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
            current.GetComponent<Projectile>().Launch(launchVector);
            canFire = false;
            ChangeState(EnemyState.walk);
            anim.SetBool(WAKE_UP, true);
        }
    }

    //public override bool TargetIsOutsideRange()
    //{
    //    return base.TargetIsOutsideRange();
    //}

    //public override void GoBackToSleep()
    //{
    //    base.GoBackToSleep();
    //}








}
