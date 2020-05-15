using UnityEngine;

public class Heart : Powerup
{
    private const string PLAYER_TAG = "Player";
    private const float HEALTH_MULTIPLIER = 2f;

    [Header("Health stats")]
    public FloatValue playerHealth;
    public float amountToIncrease;
    public FloatValue heartContainers;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG) && !other.isTrigger)
        {

            playerHealth.RuntimeValue += amountToIncrease;
            if (playerHealth.initialValue > heartContainers.RuntimeValue * HEALTH_MULTIPLIER)
            {
                playerHealth.initialValue = heartContainers.RuntimeValue * HEALTH_MULTIPLIER;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
