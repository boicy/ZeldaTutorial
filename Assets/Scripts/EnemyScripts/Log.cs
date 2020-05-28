using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public const string PLAYER_TAG = "Player";
    public const string WAKE_UP = "wakeUp";
    public const string MOVE_X = "moveX";
    public const string MOVE_Y = "moveY";

    [Header("Physics")]
    public Rigidbody2D myRigidBody;

    [Header("Animator")]
    public Animator anim;

    [Header("Target variables")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;    

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag(PLAYER_TAG).transform;
        anim = GetComponent<Animator>();
        anim.SetBool(WAKE_UP, true);        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    protected override bool TheTargetIsInRangeToChase()
    {
        return Vector3.Distance(target.position, transform.position) <= chaseRadius
                    && Vector3.Distance(target.position, transform.position) > attackRadius;
    }

    protected override bool InAValidStateToChaseTarget()
    {
        return currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger;
    }

    protected override void DoChasingBehaviour()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position,
                                                    target.position,
                                                    moveSpeed * Time.deltaTime);
        changeAmim(temp - transform.position);
        myRigidBody.MovePosition(temp);
        ChangeState(EnemyState.walk);
        anim.SetBool(WAKE_UP, true);
    }

    protected override bool TargetIsInsideRangeToChaseAndAttack()
    {
        //weird state machine thing going on here
        return false;
    }

    protected override void DoAttackingBehaviour()
    {
        //this is never reached in base log class as default is to not to do
        //this just run at them and do damage that way. Hopefully other
        // enemies will use this as a default too!
        throw new NotImplementedException();
    }

    protected override bool TargetIsOutsideRangeToChase()
    {
        return Vector3.Distance(target.position, transform.position) > chaseRadius;
    }

    protected override void DoRestingBehaviour()
    {
        anim.SetBool(WAKE_UP, false);
    }

    public void setAnimFloat(Vector2 setVector)
    {
        anim.SetFloat(MOVE_X, setVector.x);
        anim.SetFloat(MOVE_Y, setVector.y);
    }

    public void changeAmim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
            if (direction.x > 0)
            {
                setAnimFloat(Vector2.right);   
            } else if(direction.x < 0)
            {
                setAnimFloat(Vector2.left);
            }

        } else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
            if (direction.y > 0)
            {
                setAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                setAnimFloat(Vector2.down);
            }
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
