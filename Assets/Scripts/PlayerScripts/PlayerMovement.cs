using System.Collections;
using UnityEngine;

public enum PlayerState {
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour {

    private const string MOVE_X = "moveX";
    private const string MOVE_Y = "moveY";
    private const string MOVING = "moving";
    private const float THIRD_OF_A_SECOND = 0.3f;
    private const int MIN_HEALTH = 0;

    [Header("State")]
    public PlayerState currentState;
    
    [Header("Movement")]
    public VectorValue startingPosition;
    public float speed;

    [Header("Health stats")]
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public SignalSender playerHit;

    [Header("Inventory stuff")]
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start () {
        currentState = PlayerState.walk;
        myRigidBody = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
        animator.SetFloat(MOVE_X, 1);
        animator.SetFloat(MOVE_Y, -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update ()
    {
        //is the player in an interaction
        if (currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (IsValidAttack())
        {
            StartCoroutine(AttackCoroutine());
        }
        else if (NotAttacking())
        {
            UpdateAnimationAndMove();
        }
    }

    private bool NotAttacking()
    {
        return currentState == PlayerState.walk || currentState == PlayerState.idle;
    }

    private bool IsValidAttack()
    {
        return Input.GetButtonDown("attack")
                    && currentState != PlayerState.attack
                    && currentState != PlayerState.stagger;
    }

    void UpdateAnimationAndMove () {
        if (change != Vector3.zero) {
            MovePlayer ();
            animator.SetFloat (MOVE_X, change.x);
            animator.SetFloat (MOVE_Y, change.y);
            animator.SetBool (MOVING, true);
        } else {
            animator.SetBool (MOVING, false);
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
        yield return new WaitForSeconds (THIRD_OF_A_SECOND);
        if (currentState != PlayerState.interact) { 
            currentState = PlayerState.walk;
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (PlayerIsAlive())
        {
            StartCoroutine(knockCoroutine(knockTime));
        }
        else
        {
            PlayerDies();
        }
    }

    private void PlayerDies()
    {
        gameObject.SetActive(false);
    }

    private bool PlayerIsAlive()
    {
        return currentHealth.RuntimeValue > MIN_HEALTH;
    }

    private IEnumerator knockCoroutine(float knockTime)
    {
        playerHit.Raise();
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

}