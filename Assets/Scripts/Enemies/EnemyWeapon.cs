using UnityEngine;
using System.Collections;

public class EnemyWeapon : WeaponController
{
    [SerializeField] protected float lifetime = 1f;

    protected float timeAlive = 0f;

    protected override void HitObject(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            base.HitObject(collider);
        }
    }

    protected virtual void Update()
    {
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
        timeAlive += Time.deltaTime;
    }

    public virtual void MoveWeapon(){ }
}
