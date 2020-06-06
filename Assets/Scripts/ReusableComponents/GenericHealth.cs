using UnityEngine;

public class GenericHealth : MonoBehaviour
{

    public FloatValue maxHealth;
    [SerializeField] protected float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        FullHeal();
    }

    public virtual void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0)
        {
            InstaKill();
        }
    }

    public virtual void InstaKill()
    {
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
