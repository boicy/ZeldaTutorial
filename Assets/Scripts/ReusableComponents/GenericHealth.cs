using UnityEngine;

public class GenericHealth : MonoBehaviour
{

    public FloatValue maxHealth;
    [SerializeField] protected float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        FullHeal();
        Debug.Log("Starting with Generic health: maxHealth Runtime: " + maxHealth.RuntimeValue);
        Debug.Log("Starting with Generic health: maxHealth Initial: " + maxHealth.initialValue);
        Debug.Log("Starting with Generic health: currentHealth: " + currentHealth);
    }

    public virtual void Damage(float damageAmount)
    {
        Debug.Log("Generic health: damage taken: " + damageAmount);
        currentHealth -= damageAmount;
        Debug.Log("Generic health: currentHealth after damage" + currentHealth);
        if (currentHealth < 0)
        {           
            InstaKill();
        }                
    }

    public virtual void InstaKill()
    {
        Debug.Log("Generic health: Instakill" + currentHealth);
        currentHealth = 0;
    }

    public virtual void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth.RuntimeValue)
        {
            FullHeal();
        }
    }

    public virtual void FullHeal()
    {
        currentHealth = maxHealth.RuntimeValue;
    }
}
