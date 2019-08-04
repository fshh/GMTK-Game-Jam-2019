using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D target;
    [SerializeField] [Range(0f, 1f)] private float acceleration = 0.8f;
    [SerializeField] [Range(0f, 3f)] private float drag = 0.1f;
    [SerializeField] [Range(0f, 3f)] private float angularDrag = 0.5f;

    private ParticleSystem particles;
    private AudioSource sound;

    private void Awake() {
        particles = GetComponentInChildren<ParticleSystem>();
        sound = GetComponent<AudioSource>();
        particles.Stop();
    }

    void FixedUpdate()
    {
        if (!PlayerController.IsDead()) {
            if (Input.GetMouseButton(0)) {
                particles.Play();
                sound.Play();
                target.velocity += (Vector2)(transform.position - target.transform.position).normalized * acceleration;
                target.angularVelocity += Mathf.Sign(target.angularVelocity) * target.velocity.magnitude;
            } else {
                particles.Stop();
                sound.Stop();
            }

            target.drag = drag;
            target.angularDrag = angularDrag;
        } else {
            particles.Stop();
            sound.Stop();
        }
    }
}
