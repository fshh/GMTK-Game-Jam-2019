using UnityEngine;
using System.Collections;

public class SeekerWeapon : EnemyWeapon
{
    [SerializeField] private float hitboxStartTime;
    [SerializeField] private float hitboxEndTime;

    private Collider2D hitbox;

    private void Start() {
        GetComponentInChildren<Animator>().SetTrigger("Attack");
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
    }

    public override void MoveWeapon()
    {
        hitbox.enabled = timeAlive > hitboxStartTime && timeAlive < hitboxEndTime;
    }
}
