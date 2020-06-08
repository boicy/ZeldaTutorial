using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Dash Ability", fileName = "Dash Ability")]
public class DashAbility : GenericAbility
{
    public float dashForce;

    public override void Ability(Vector2 playerPosition, Vector2 playerFacingDirection = default,
                                 Animator playerAnimator = null, Rigidbody2D playerRigidBody = null)
    {
        //Make sure enough magic
        if (playerMagic.RuntimeValue >= magicCost)
        {
            playerMagic.RuntimeValue -= magicCost;
            usePlayerMagic.Raise();
        }
        else
        {
            return;
        }
        if (playerRigidBody)
        {
            Vector3 dashVector = playerRigidBody.transform.position
                + (Vector3)playerFacingDirection.normalized * dashForce;
            playerRigidBody.DOMove(dashVector, duration);
        }
    }
}
