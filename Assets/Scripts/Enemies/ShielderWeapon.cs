using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShielderWeapon : EnemyWeapon
{
    [SerializeField] private float hitboxStartTime;
    [SerializeField] private float hitboxEndTime;

    private Collider2D hitbox;

    // This is a bit of a hack to put the weapon at the shield's position.
    private void Start()
    {
        GetComponent<Animator>().SetTrigger("Attack");
        transform.localPosition = transform.parent.GetChild(0).localPosition;
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }

    public override void MoveWeapon()
    {
        hitbox.enabled = timeAlive > hitboxStartTime && timeAlive < hitboxEndTime;
    }
}
