using UnityEngine;

public class Interactable : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    
    public bool playerInRange;
    public SignalSender context;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;            
        }
    }
}
