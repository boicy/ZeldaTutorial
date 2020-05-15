using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private const string PLAYER_TAG = "Player";
    private const string WAKE_UP = "wakeUp";
    private const string MOVE_X = "moveX";
    private const string MOVE_Y = "moveY";

    [Header("Physics")]
    public Rigidbody2D myRigidBody;
    public Animator anim;

    [Header("Attack Stats")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    [Header("Locations")]
    public Transform homePosition;    

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

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                                                            target.position,
                                                            moveSpeed * Time.deltaTime);
                changeAmim(temp - transform.position);
                myRigidBody.MovePosition(temp);                
                ChangeState(EnemyState.walk);
                anim.SetBool(WAKE_UP, true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool(WAKE_UP, false);
        }
    }

    private void setAnimFloat(Vector2 setVector)
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

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
