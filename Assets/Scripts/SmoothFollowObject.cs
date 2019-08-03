using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothFollowObject : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] [Range(0f, 0.2f)] private float smoothTime;

    private Image img;
    private Vector2 velocity = Vector2.zero;

    private void Awake() {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target) {
            img.enabled = false;
            return;
        } else {
            img.enabled = true;
        }
        transform.position = Vector2.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }
}
