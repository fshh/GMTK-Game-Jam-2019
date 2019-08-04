using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : Enemy
{
    [SerializeField] [Range(0f,1f)] private float attackMovementSpeedModifier = 0.5f;

    protected override void AttackMovement() {
        base.NormalMovement();
        rb.velocity *= attackMovementSpeedModifier;
    }
}
