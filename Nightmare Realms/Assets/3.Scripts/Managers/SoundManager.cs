using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip coin;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // PlayBGMLoop();
    }

    public void PlayBGMLoop(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}