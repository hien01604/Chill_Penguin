using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // tao bien luu tru audio source
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    // tao bien luu audio clip
    public AudioClip musicClip;
    public AudioClip flyClip;

    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();

    }
}

