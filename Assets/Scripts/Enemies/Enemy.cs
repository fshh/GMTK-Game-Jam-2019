using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{
    [SerializeField] public EnemyType type = EnemyType.Seeker;
    [SerializeField] protected float speed = 4f;
    [SerializeField] [Range(0f, 1f)] protected float deathShake = 0.2f;
    [SerializeField] [Range(0f, 1f)] protected float hitStunDuration = 0.1f;
    [SerializeField] protected float attackDistance = 1f;
    [SerializeField] protected EnemyWeapon weaponPrefab;
    [SerializeField] protected AudioClip death;

    public GameObject Target { get; set; }
    public bool Visible => GetComponent<SpriteRenderer>().enabled;
    protected virtual bool Attacking => weapon != null;

    protected EnemyWeapon weapon;

    protected Rigidbody2D rb;
    protected Animator anim;

    /// <summary>
    /// Use for initializations.
    /// </summary>
    protected virtual void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (!Target)
            return;

        if ((Vector3.Distance(transform.position, Target.transform.position) < attackDistance) && !Attacking)
        {
            Attack();
        }
    }
    
    /// <summary>
    /// Overload to change how movement behaves.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        if (Attacking)
        {
            AttackMovement();
            weapon?.MoveWeapon();
        }
        else
        {
            NormalMovement();
        }
    }

    protected virtual void Attack()
    {
        weapon = Instantiate<EnemyWeapon>(weaponPrefab, transform);
    }

    protected virtual void NormalMovement()
    {
        Vector3 direction = GetDirection();
        rb.velocity = direction * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward).eulerAngles.z;
    }

    protected virtual void AttackMovement()
    {
        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Determines the direction the enemy should go this fixed update. 
    /// Feel free to change the speed here too.
    /// </summary>
    /// <returns>The direction the enemey should head.</returns>
    protected virtual Vector3 GetDirection()
    {
        if (!Target) {
            if (!this.GetType().IsSubclassOf(typeof(Enemy))) {
                anim.SetBool("Walking", false);
            }
            return Vector3.zero;
        }
        if (!this.GetType().IsSubclassOf(typeof(Enemy))) {
            anim.SetBool("Walking", !Attacking);
        }
        return (Target.transform.position - transform.position).normalized;
    }

    public virtual void SetVisibility(bool visible)
    {
        GetComponent<SpriteRenderer>().enabled = visible;
    }

    public virtual void OnHit() 
    {
        CameraController cam = Camera.main.GetComponent<CameraController>();
        cam.InduceStress(deathShake);
        StartCoroutine(HitStun());
    }

    private IEnumerator HitStun() {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(hitStunDuration);
        Time.timeScale = 1f;
        AudioSource deathsound = Instantiate(Resources.Load<AudioSource>("Prefabs/Death Sound"), transform.position, Quaternion.identity);
        deathsound.GetComponent<DestroyAfterDelay>().delay = death.length;
        deathsound.clip = death;
        deathsound.pitch = Random.Range(0.5f, 1.5f);
        deathsound.Play();
        FindObjectOfType<EnemyController>().RemoveEnemy(this);
    }
}
