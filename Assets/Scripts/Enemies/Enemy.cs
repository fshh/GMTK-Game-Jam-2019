using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{
    public string type = "Default";
    [SerializeField] protected float speed = 4f;

    public GameObject Target { get; set; }
    public bool Visible => GetComponent<SpriteRenderer>().enabled;

    private Rigidbody2D rb;
    
    /// <summary>
    /// Use for initializations.
    /// </summary>
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    /// <summary>
    /// Overload to change how movement behaves.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        Vector3 direction = GetDirection();
        rb.velocity = direction * speed;
    }

    /// <summary>
    /// Determines the direction the enemy should go this fixed update. 
    /// Feel free to change the speed here too.
    /// </summary>
    /// <returns>The direction the enemey should head.</returns>
    protected virtual Vector3 GetDirection()
    {
        return (Target.transform.position - transform.position).normalized;
    }

    public virtual void SetVisibility(bool visible)
    {
        GetComponent<SpriteRenderer>().enabled = visible;
    }

    public void OnHit()
    {
        Destroy(this.gameObject);
    }
}
