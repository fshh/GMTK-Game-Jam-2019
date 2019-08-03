using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokesterWeapon : EnemyWeapon
{
    [SerializeField] private float minRange = 0.2f;
    [SerializeField] private float maxRange = 0.7f;

    public override void MoveWeapon()
    {
        transform.localPosition = new Vector2(0, Mathf.Lerp(minRange, maxRange, timeAlive / lifetime));
    }
}
