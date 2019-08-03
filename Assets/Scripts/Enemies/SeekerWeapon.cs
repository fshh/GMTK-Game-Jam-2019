using UnityEngine;
using System.Collections;

public class SeekerWeapon : EnemyWeapon
{
    [SerializeField] private float startingRot;
    [SerializeField] private float endingRot;

    public override void MoveWeapon()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(startingRot, endingRot, timeAlive / lifetime)));
    }
}
