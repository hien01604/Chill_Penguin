using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance for global access

    // Tạo biến lưu trữ AudioSource
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    // Tạo biến lưu AudioClip
    public AudioClip musicClip;
    public AudioClip flyClip;
    public AudioClip clickClip;
    public AudioClip gameOverClip;

    private void Awake()
    {
        // Ensure a single instance of AudioManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist this object across scenes
    }

    private void Start()
    {
        // Phát nhạc nền
        if (musicAudioSource != null && musicClip != null)
        {
            musicAudioSource.clip = musicClip;
            musicAudioSource.loop = true; // Lặp lại nhạc nền
            musicAudioSource.Play();
        }
    }

    // Hàm phát âm thanh khi bấm nút
    public void PlayButtonClickSound()
    {
        if (vfxAudioSource != null && clickClip != null)
        {
            vfxAudioSource.PlayOneShot(clickClip);
        }
        else
        {
            Debug.LogWarning("vfxAudioSource hoặc clickClip chưa được gán!");
        }
    }

    // Hàm để đổi nhạc (ví dụ khi vào Game Over)
    public void PlayMusic(AudioClip newMusicClip)
    {
        if (musicAudioSource != null && newMusicClip != null)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = newMusicClip;
            musicAudioSource.loop = true;
            musicAudioSource.Play();
        }
    }

    // Hàm dừng nhạc nền
    public void StopMusic()
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.Stop();
        }
    }
}
