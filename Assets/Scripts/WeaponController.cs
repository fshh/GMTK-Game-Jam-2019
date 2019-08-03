using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IKillable hitObject = collision.collider.GetComponent<IKillable>();
        if (hitObject != null)
        {
            hitObject.OnHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IKillable hitObject = collision.GetComponent<IKillable>();
        if (hitObject != null)
        {
            Debug.Log(hitObject);
            hitObject.OnHit();
        }
    }
}
