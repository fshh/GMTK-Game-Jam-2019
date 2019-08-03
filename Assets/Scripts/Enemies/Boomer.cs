using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : Enemy
{
    [SerializeField] private float fuse = 1f;
    [SerializeField] private float explosionForce = 10;
    [SerializeField] private float explosionRadius;

    private bool attacking = false;
    protected override bool Attacking => attacking;

    public override void OnHit()
    {
        Attack();
    }

    protected override void Attack()
    {
        StartCoroutine("Explode");
        attacking = true;
    }

    protected IEnumerator Explode()
    {
        yield return new WaitForSeconds(fuse);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Made " + collider + " go boom");
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            collider.attachedRigidbody.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            if (collider != GetComponent<Collider2D>())
            {
                collider.GetComponent<IKillable>()?.OnHit();
            }
        }
        base.OnHit();
    }
}
