using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable
{
    [SerializeField] [Range(0f, 1f)] protected float hitStunDuration = 1f;

    public void OnHit()
    {
        CameraController cam = Camera.main.GetComponent<CameraController>();
        cam.InduceStress(0.5f);
        StartCoroutine(HitStun());
    }

    private IEnumerator HitStun() {
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(hitStunDuration);
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }
}
