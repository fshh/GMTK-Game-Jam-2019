using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable
{
    [SerializeField] [Range(0f, 1f)] protected float cameraStress = 0.5f;
    [SerializeField] [Range(0f, 1f)] protected float hitStunDuration = 1f;
    [SerializeField] [Range(0f, 1f)] protected float timeScale = 0.1f;

    public static bool dead = false;

    private void Start()
    {
        dead = false;
    }

    public void OnHit()
    {
        if (!dead) {
            dead = true;
            CameraController cam = Camera.main.GetComponent<CameraController>();
            cam.InduceStress(cameraStress);
            StartCoroutine(HitStun());
        }
    }

    private IEnumerator HitStun() {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(hitStunDuration);
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }

    public static bool IsDead() {
        return dead;
    }
}
