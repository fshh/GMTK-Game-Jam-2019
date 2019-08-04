using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer : Enemy
{
    [SerializeField] private float fuse = 1f;
    [SerializeField] private float explosionForce = 10;
    [SerializeField] private float explosionRadius;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject spotlight;
    [SerializeField] protected GameObject enemyBloodSpray;
    [SerializeField] protected GameObject playerBloodSpray;

    private bool attacking = false;
    protected override bool Attacking => attacking;

    protected override void Start() {
        anim = GetComponent<Animator>();
        base.Start();
    }

    protected override Vector3 GetDirection() {
        if (!Target) {
            anim.SetBool("Walking", false);
        } else {
            anim.SetBool("Walking", true);
        }
        return base.GetDirection();
    }

    public override void OnHit()
    {
        Attack();
        CameraController cam = Camera.main.GetComponent<CameraController>();
        cam.InduceStress(deathShake);
        StartCoroutine(HitStun());
    }

    protected override void Attack()
    {
        StartCoroutine("Explode");
        attacking = true;
    }

    protected IEnumerator Explode()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        GameObject light = Instantiate(spotlight, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Spotlight Canvas").transform);
        light.transform.SetAsFirstSibling();

        yield return new WaitForSeconds(fuse);

        Instantiate(explosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/Boom"), transform.position, 3f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Made " + collider + " go boom");
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            collider.attachedRigidbody?.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            if (collider != GetComponent<Collider2D>())
            {
                GameObject bloodPrefab = collider.tag == "Player" ? playerBloodSpray : enemyBloodSpray;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                Instantiate(bloodPrefab, collider.transform.position, rotation);

                collider.GetComponent<IKillable>()?.OnHit();
            }
        }
        base.OnHit();
    }

    private IEnumerator HitStun() {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(hitStunDuration);
        Time.timeScale = 1f;
    }
}
