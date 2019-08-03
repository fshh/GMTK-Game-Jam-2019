using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] protected float speedToKill;

    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitObject(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitObject(collision);
    }

    protected virtual void HitObject(Collider2D collider)
    {
        IKillable hitObject = collider.GetComponent<IKillable>();
        if (hitObject != null && rb.velocity.magnitude > speedToKill)
        {
            hitObject.OnHit();
        }
    }
}
