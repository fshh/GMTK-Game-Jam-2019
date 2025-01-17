﻿using System.Collections;
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
            transform.Find("Magnet").GetComponent<SpriteRenderer>().enabled = false;
            Transform head = transform.Find("Head");
            head.parent = null;
            Rigidbody2D headRB = head.gameObject.AddComponent<Rigidbody2D>();
            headRB.gravityScale = 0f;
            headRB.drag = 1f;
            headRB.velocity = GetComponent<Rigidbody2D>().velocity * 2f;

            CameraController cam = Camera.main.GetComponent<CameraController>();
            cam.InduceStress(cameraStress);
            AudioSource deathsound = Instantiate(Resources.Load<AudioSource>("Prefabs/Death Sound"), transform.position, Quaternion.identity);
            deathsound.GetComponent<DestroyAfterDelay>().delay = deathsound.clip.length;
            deathsound.Play();
            StartCoroutine(HitStun());
        }
    }

    private IEnumerator HitStun() {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(hitStunDuration);
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Audio Listener").transform.parent = null;
        //Destroy(this.gameObject);
    }

    public static bool IsDead() {
        return dead;
    }
}
