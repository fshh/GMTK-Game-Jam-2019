using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] [Range(0f, 1f)] private float acceleration = 0.3f;

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (!PlayerController.IsDead()) {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rb.velocity = Vector2.Lerp(rb.velocity, movement * speed, acceleration);
            anim.SetBool("Walking", movement.magnitude > 0f);
        } else {
            rb.rotation = 0f;
            anim.SetTrigger("Die");
        }
    }
}
