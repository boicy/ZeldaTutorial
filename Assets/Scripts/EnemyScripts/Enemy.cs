using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public abstract class Enemy : MonoBehaviour
{
    private const int MIN_HEALTH = 0;
    private const int SINGLE_FRAME_DURATION = 1;

    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    //TODO remove this
    //public int baseAttack;

    public float moveSpeed;
    public Vector2 homePosition;

    [Header("Death Effects")]
    public GameObject deathEffect;
    public LootTable thisLoot;

    [Header("Death Signals")]
    public SignalSender roomSignal;

    private void Awake()
    {
        health = maxHealth.initialValue;        
    }

    private void OnEnable()
    {
        transform.position = homePosition;
        health = maxHealth.initialValue;
        currentState = EnemyState.idle;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= MIN_HEALTH)
        {
            DeathEffect();
            MakeLoot();
            this.gameObject.SetActive(false);
            if (roomSignal != null) { 
                roomSignal.Raise();
            }
        }
    }

    private void DeathEffect()
    {
        if (deathEffect!=null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);             
            Destroy(effect, SINGLE_FRAME_DURATION);
        }
    }

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            Powerup current = thisLoot.LootPowerup();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        StartCoroutine(knockCoroutine(myRigidBody, knockTime));
        TakeDamage(damage); 
    }

    private IEnumerator knockCoroutine(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

    //lift up as virtual
    public virtual void CheckDistance()
    {
        if (TheTargetIsInRangeToChase())
        {
            if (InAValidStateToChaseTarget())
            {
                DoChasingBehaviour();
            }
        }
        else if (TargetIsInsideRangeToChaseAndAttack())
        {
            DoAttackingBehaviour();
        }
        else if (TargetIsOutsideRangeToChase())
        {
            DoRestingBehaviour();
        }
    }

    protected abstract bool TheTargetIsInRangeToChase();
    protected abstract bool InAValidStateToChaseTarget();
    protected abstract void DoChasingBehaviour();
    protected abstract bool TargetIsInsideRangeToChaseAndAttack();
    protected abstract void DoAttackingBehaviour();
    protected abstract bool TargetIsOutsideRangeToChase();
    protected abstract void DoRestingBehaviour();
}
