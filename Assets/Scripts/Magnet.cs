using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D target;
    [SerializeField] [Range(0f, 1f)] private float acceleration = 0.8f;
    [SerializeField] [Range(0f, 3f)] private float drag = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            //target.velocity = Vector2.Lerp(target.velocity, (transform.position - target.transform.position).normalized * topSpeed, acceleration);
            target.velocity += (Vector2)(transform.position - target.transform.position).normalized * acceleration;
        }

        target.drag = drag;
        //target.velocity = Vector2.Lerp(target.velocity, Vector2.zero, deceleration);
        //target.velocity -= target.velocity * deceleration;
    }
}
