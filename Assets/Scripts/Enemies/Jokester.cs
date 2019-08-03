﻿using UnityEngine;
using System.Collections;

public class Jokester : Enemy
{
    [SerializeField] private float retreatSpeed = 5f;
    /// <summary>
    /// The distance the enemy starts retreating.
    /// </summary>
    [SerializeField] private float closeDistFromPlayer = 3f;
    /// <summary>
    /// The distance the enemy starts tracking the player again.
    /// </summary>
    [SerializeField] private float farDistFromPlayer = 10f;

    private float baseSpeed = 0;
    private bool retreating = false;

    protected override void Start()
    {
        base.Start();
        baseSpeed = speed;
    }

    protected override Vector3 GetDirection()
    {
        if (!Target) {
            return Vector3.zero;
        }

        Vector3 direction = base.GetDirection();
        float distToPlayer = Vector3.Distance(transform.position, Target.transform.position);
        retreating = (distToPlayer < closeDistFromPlayer) || (retreating && distToPlayer < farDistFromPlayer);
        if (retreating)
        {
            direction *= -1;
            speed = retreatSpeed;
        }
        else
        {
            speed = baseSpeed;
        }
        return direction;
    }
}
