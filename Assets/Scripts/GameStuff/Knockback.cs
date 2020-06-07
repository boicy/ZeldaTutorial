using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    private const string ENEMY_TAG = "enemies";

    [Header("Knock stats")]
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;

    [Header("Which tag to knock")]
    [SerializeField] private string otherTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
         * Going to replace this with new damage system
        if (other.gameObject.CompareTag(BREAKABLE_TAG) && this.gameObject.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<Pots>().Smash();
        }
        */
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
            if (hit != null)
            {
                
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                //hit.AddForce(difference, ForceMode2D.Impulse);
                hit.DOMove(hit.transform.position + difference, knockTime);
                
                if (other.gameObject.CompareTag(ENEMY_TAG) && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                }
                
                if (other.GetComponentInParent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponentInParent<PlayerMovement>().Knock(knockTime);
                }
            }
        }
    }
}
