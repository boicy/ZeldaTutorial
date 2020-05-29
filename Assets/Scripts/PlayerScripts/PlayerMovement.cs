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

    [Header("Shooting stuff")]
    public GameObject projectile;
    public SignalSender reduceMagic;

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
        RunPlayerStateMachine();
    }

    private void RunPlayerStateMachine()
    {
        if (IsValidAttackFor("First Weapon"))
        {
            StartCoroutine(FirstAttackCoroutine());
        }
        else if (IsValidAttackFor("Second Weapon"))
        {
            StartCoroutine(SecondAttackCoroutine());
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

    private bool IsValidAttackFor(string weaponType)
    {
        return Input.GetButtonDown(weaponType)
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

    private IEnumerator FirstAttackCoroutine() {        
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool ("attacking", false);
        yield return new WaitForSeconds (THIRD_OF_A_SECOND);
        if (currentState != PlayerState.interact) { 
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator SecondAttackCoroutine()
    {        
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();        
        yield return new WaitForSeconds(THIRD_OF_A_SECOND);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private void MakeArrow()
    {
        if (playerInventory.currentMagic > 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat(MOVE_X), animator.GetFloat(MOVE_Y));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseArrowDirection());
            playerInventory.ReduceMagic(arrow.magicCost);
            reduceMagic.Raise();
        }
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat(MOVE_Y), animator.GetFloat(MOVE_X)) * Mathf.Rad2Deg;
        return new Vector3(0,0,temp);
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