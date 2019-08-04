using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private float looptime;

    private AudioSource music;

    private void Start()
    {
        music = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (music.time >= looptime)
        {
            music.Play();
        }
    }
}
