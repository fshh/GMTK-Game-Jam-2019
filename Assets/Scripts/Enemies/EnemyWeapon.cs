using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] protected float lifetime = 1f;
    [SerializeField] protected GameObject playerBloodSpray;

    protected float timeAlive = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !PlayerController.IsDead())
        {
            Vector2 dir = ((collision.transform.position - transform.root.position) + transform.root.up).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Instantiate(playerBloodSpray, collision.transform.position, rotation);

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
