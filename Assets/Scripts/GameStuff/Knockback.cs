using UnityEngine;

public class Knockback : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string BREAKABLE_TAG = "breakable";
    private const string ENEMY_TAG = "enemies";

    [Header("Knock stats")]
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(BREAKABLE_TAG) && this.gameObject.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<Pots>().Smash();
        }

        if (other.gameObject.CompareTag(ENEMY_TAG) || other.gameObject.CompareTag(PLAYER_TAG))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag(ENEMY_TAG) && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag(PLAYER_TAG))
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}
