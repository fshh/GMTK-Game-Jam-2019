using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] [Range(0f, 0.2f)] private float smoothTime;

    private Vector2 velocity = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }
}
