using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource bgmSource;
    public AudioSource sfxSource_1;
    public AudioSource sfxSource_2;

    public AudioClip bgmMain;
    public AudioClip bgmStage_1;
    public AudioClip bgmStage_2;
    public AudioClip bgmStage_3;
    public AudioClip bgmStage_4;
    public AudioClip bgmStage_5;

    public AudioClip move;
    public AudioClip coin;
    public AudioClip swordHit;
    public AudioClip jump;
    public AudioClip click;
    public AudioClip shopSpin;
    public AudioClip shopItemPurchase;
    public AudioClip error;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGMLoop(bgmMain);
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

    public void PlaySFXLoop(AudioClip clip)
    {
        if (sfxSource_1.clip != clip)
        {
            sfxSource_1.clip = clip;
            sfxSource_1.Play();
        }
    }
}