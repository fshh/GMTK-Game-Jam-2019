using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D target;
    [SerializeField] [Range(0f, 1f)] private float acceleration = 0.8f;
    [SerializeField] [Range(0f, 3f)] private float drag = 0.1f;
    [SerializeField] [Range(0f, 3f)] private float angularDrag = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            target.velocity += (Vector2)(transform.position - target.transform.position).normalized * acceleration;
            target.angularVelocity += Mathf.Sign(target.angularVelocity) * target.velocity.magnitude;
        }

        target.drag = drag;
        target.angularDrag = angularDrag;
    }
}
