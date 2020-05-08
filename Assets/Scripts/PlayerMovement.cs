using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour {

    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start () {
        currentState = PlayerState.walk;
        myRigidBody = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
        animator.SetFloat("moveX", 1);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update () {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw ("Horizontal");
        change.y = Input.GetAxisRaw ("Vertical");
        if (Input.GetButtonDown ("attack") && currentState != PlayerState.attack) {
            StartCoroutine (AttackCoroutine ());
        } else if (currentState == PlayerState.walk) {
            UpdateAnimationAndMove ();
        }
    }

    void UpdateAnimationAndMove () {
        if (change != Vector3.zero) {
            MovePlayer ();
            animator.SetFloat ("moveX", change.x);
            animator.SetFloat ("moveY", change.y);
            animator.SetBool ("moving", true);
        } else {
            animator.SetBool ("moving", false);
        };
    }

    void MovePlayer()
    {
        change.Normalize();
        myRigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    private IEnumerator AttackCoroutine() {        
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool ("attacking", false);
        yield return new WaitForSeconds (0.3f);
        currentState = PlayerState.walk;
    }


}