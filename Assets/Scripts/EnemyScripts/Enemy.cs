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

public class Enemy : MonoBehaviour
{
    private const int MIN_HEALTH = 0;
    private const int SINGLE_FRAME_DURATION = 1;

    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    [Header("Death Effects")]
    public GameObject deathEffect;

    private void Awake()
    {
        health = maxHealth.initialValue ;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= MIN_HEALTH)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
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
}
