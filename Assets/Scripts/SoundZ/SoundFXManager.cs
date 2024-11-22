using System;
using System.Collections;
using System.Collections.Generic;
using Tech.Singleton;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SoundType
{
    Game,
    Music,
    UI,
}
public class SoundFXManager : Singleton<SoundFXManager>
{
    public AudioSource GameSource;
    public AudioSource MusicSource;
    public AudioSource UISource;

    public void PlaySound(AudioClip clip, SoundType soundType,float volume = 1f)
    {
        AudioSource audioSource = null;
        switch (soundType)
        {
            case SoundType.Game:
                audioSource = GameSource;
                break;
            case SoundType.Music:
                audioSource = MusicSource;
                break;
            case SoundType.UI:
                audioSource = UISource;
                break;
            default:
                audioSource = GameSource;
                break;
        }
        audioSource.PlayOneShot(clip, volume);
    }
    public void PlayRandomSound(AudioClip[] clips, SoundType soundType,float volume = 1f)
    {
        PlaySound(clips[Random.Range(0, clips.Length)], soundType, volume);
    }
    public void PlayRandomSound(List<AudioClip> clips, SoundType soundType,float volume = 1f)
    {
        PlaySound(clips[Random.Range(0, clips.Count)], soundType, volume);
    }
}
