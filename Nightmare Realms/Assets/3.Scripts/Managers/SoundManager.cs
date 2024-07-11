using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource_1;
    [SerializeField] private AudioSource sfxSource_2;

    public AudioClip coin;
    public AudioClip swordHit;

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

    public void PlaySFX_1(AudioClip clip)
    {
        sfxSource_1.PlayOneShot(clip);
    }

    public void PlaySFX_2(AudioClip clip)
    {
        sfxSource_2.PlayOneShot(clip);
    }
}