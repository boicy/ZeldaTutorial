using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;


    public Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 initialVelocity)
    {
        myRigidBody.velocity = initialVelocity * moveSpeed;
    }    
}
