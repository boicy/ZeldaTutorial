using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : Interactable
{
    private const int WALK_RIGHT = 0;
    private const int WALK_UP = 1;
    private const int WALK_LEFT = 2;
    private const int WALK_DOWN = 3;
    private Vector3 directionVector;
    private Transform myTransform;
    private Rigidbody2D myRigidBody;
    private Animator anim;    

    [Header("Bounds")]
    public Collider2D bounds;

    [Header("Speed")]
    public float speed;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeDirection();
    }
    
    void Update()
    {
        if (!playerInRange)
        {
            Move();
        }
        
    }

    private void Move()
    {
        Vector3 destination = myTransform.position + directionVector * speed * Time.deltaTime;

        if (bounds.bounds.Contains(destination))
        {
            myRigidBody.MovePosition(destination);
        }
        else
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case WALK_RIGHT:
                directionVector = Vector3.right;
                break;
            case WALK_UP:
                directionVector = Vector3.up;
                break;
            case WALK_LEFT:
                directionVector = Vector3.left;
                break;
            case WALK_DOWN:
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        anim.SetFloat("moveX", directionVector.x);
        anim.SetFloat("moveY", directionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var currentDirection = directionVector;
        ChangeDirection();
        int loops = 0;
        while (currentDirection == directionVector && loops < 10) 
        {
            loops++;
            ChangeDirection();
        }
    }
}
