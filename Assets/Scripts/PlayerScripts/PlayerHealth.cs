using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GenericHealth
{

    [SerializeField] private SignalSender healthSignal;

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        maxHealth.RuntimeValue = currentHealth;
        healthSignal.Raise();        
    }
}
