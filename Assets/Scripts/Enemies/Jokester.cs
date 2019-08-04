using UnityEngine;
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
        anim = GetComponent<Animator>();
        base.Start();
        baseSpeed = speed;
    }

    protected override Vector3 GetDirection()
    {
        if (!Target) {
            anim.SetBool("Running", false);
            return Vector3.zero;
        } else {
            anim.SetBool("Running", true);
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

    protected override void Attack()
    {
        Vector3 direction = base.GetDirection();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward).eulerAngles.z;
        base.Attack();
    }
}
