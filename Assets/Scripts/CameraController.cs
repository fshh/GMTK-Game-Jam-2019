using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Following player and weapon
    [SerializeField] private Transform player;
    [SerializeField] private Transform weapon;
    [SerializeField][Range(0f,1f)] private float playerPriority = 0.8f;
    private Vector3 velocity = Vector3.zero;

    // Camera shake
    [SerializeField] Vector3 maximumTranslationShake = Vector3.one;
    [SerializeField] Vector3 maximumAngularShake = Vector3.one * 15;
    [SerializeField] float frequency = 25;
    [SerializeField] float traumaExponent = 1;
    [SerializeField] float recoverySpeed = 1;
    private float trauma;
    private float seed;

    private void Awake() {
        seed = Random.value;
    }

    public void InduceStress(float stress) {
        trauma = Mathf.Clamp01(trauma + stress);
    }

    // Update is called once per frame
    void Update()
    {
        // Follow player and weapon
        Vector3 targetPoint = weapon.position + (player.position - weapon.position) * playerPriority;
        targetPoint.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPoint, ref velocity, 0.1f);

        // Apply camera shake
        float shake = Mathf.Pow(trauma, traumaExponent);
        transform.position += new Vector3(
            maximumTranslationShake.x * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1),
            maximumTranslationShake.y * (Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1),
            maximumTranslationShake.z * (Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2 - 1)
        ) * shake;
        transform.localRotation = Quaternion.Euler(new Vector3(
            maximumAngularShake.x * (Mathf.PerlinNoise(seed + 3, Time.time * frequency) * 2 - 1),
            maximumAngularShake.y * (Mathf.PerlinNoise(seed + 4, Time.time * frequency) * 2 - 1),
            maximumAngularShake.z * (Mathf.PerlinNoise(seed + 5, Time.time * frequency) * 2 - 1)
        ) * shake);
        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
    }
}
