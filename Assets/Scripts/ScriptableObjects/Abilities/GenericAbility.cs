using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects/Abilities/Generic Ability", fileName ="New Generic Ability")]
public class GenericAbility : ScriptableObject
{

    public float magicCost;
    public float duration;

    public FloatValue playerMagic;
    public SignalSender usePlayerMagic;

    public virtual void Ability(Vector2 playerPosition, Vector2 playerFacingDirection =  default(Vector2),
        Animator playerAnimator = null, Rigidbody2D playerRigidBody = null)
    {
        
    }

}
