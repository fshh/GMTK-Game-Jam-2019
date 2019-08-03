using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] protected float lifetime = 1f;

    protected float timeAlive = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<IKillable>().OnHit();
        }
    }

    protected virtual void Update()
    {
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
        timeAlive += Time.deltaTime;
    }

    public virtual void MoveWeapon(){ }
}
