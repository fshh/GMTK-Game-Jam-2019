using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShielderWeapon : EnemyWeapon
{
    [SerializeField] private float maxWidth;

    private Vector3 initialPosition;

    // This is a bit of a hack to put the weapon at the shield's position.
    private void Start()
    {
        initialPosition = transform.parent.GetChild(0).localPosition;
        transform.localPosition = initialPosition;
    }

    public override void MoveWeapon()
    {
        transform.localScale = new Vector3(1, Mathf.Lerp(0, maxWidth, timeAlive / lifetime), 1);
        transform.localPosition = new Vector2(0, initialPosition.y + (transform.localScale.y / 2));
    }
}
