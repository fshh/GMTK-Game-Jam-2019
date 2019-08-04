﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] protected float speedToKill;
    [SerializeField] protected GameObject enemyBloodSpray;

    protected Rigidbody2D rb;
    protected AudioSource audio;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
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
        
        if (hitObject is Enemy)
        {
            audio.clip = Resources.Load<AudioClip>("Sounds/Hit" + Random.Range(1, 9));
            audio.Play();
        }
        else if (!(hitObject is PlayerController))
        {
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/ShieldHit" + Random.Range(1, 6)), transform.position);
        }

        if (hitObject != null && Mathf.Max(rb.velocity.magnitude, rb.angularVelocity) > speedToKill)
        {
            if (hitObject.GetType() == typeof(Enemy) || hitObject.GetType().IsSubclassOf(typeof(Enemy))) {
                Debug.Log("hit enemy");
                //Vector2 dir = (collider.transform.position - transform.position).normalized;
                Vector2 dir = ((Vector2)(collider.transform.position - transform.position) + rb.velocity).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                Instantiate(enemyBloodSpray, collider.transform.position, rotation);
            }
            hitObject.OnHit();
        }
    }
}
