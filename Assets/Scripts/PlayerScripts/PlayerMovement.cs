using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour {

    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public VectorValue startingPosition;

    // Start is called before the first frame update
    void Start () {
        currentState = PlayerState.walk;
        myRigidBody = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
        animator.SetFloat("moveX", 1);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update () {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw ("Horizontal");
        change.y = Input.GetAxisRaw ("Vertical");
        if (Input.GetButtonDown ("attack")
            && currentState != PlayerState.attack
            && currentState != PlayerState.stagger) {
            StartCoroutine (AttackCoroutine ());
        } else if (currentState == PlayerState.walk || currentState == PlayerState.idle) {
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

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0) {            
            StartCoroutine(knockCoroutine(knockTime));
        }else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator knockCoroutine(float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

}