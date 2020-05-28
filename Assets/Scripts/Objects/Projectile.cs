using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("Lifetime")]
    public float lifetime;
    private float lifetimeSeconds;
    public Rigidbody2D myRigidBody;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVelocity)
    {
        myRigidBody.velocity = initialVelocity * moveSpeed;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        //This is being triggered by the Room polygon collider
        //leading to the projectiles beign destroyed immediately.
        //Not that annoying or anything.
        //need to find some way to fix this
        Destroy(this.gameObject);        
    }
}
