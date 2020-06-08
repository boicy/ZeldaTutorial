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
        if (canFire == false)
        {
            fireDelaySeconds -= Time.deltaTime;
            if (fireDelaySeconds <= 0)
            {
                canFire = true;
                fireDelaySeconds = fireDelay;
            }
        }
    }

    protected override void DoChasingBehaviour()
    {
        if (canFire)
        {
            Vector3 launchVector = target.transform.position - transform.position;
            GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
            Projectile projectile1 = current.GetComponent<Projectile>();
            projectile1.Launch(launchVector);
            canFire = false;
            ChangeState(EnemyState.walk);
            anim.SetBool(WAKE_UP, true);
        }
    }
}
