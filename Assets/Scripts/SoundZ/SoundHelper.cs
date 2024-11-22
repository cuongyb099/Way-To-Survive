using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource;
using UnityEngine;
using UnityEngine.Audio;

public class SoundHelper : MonoBehaviour
{
    public AudioClip Clip;
    public SoundType Type;
    [Range(0f, 1f)] public float Volume;

    public void Play()
    {
        SoundFXManager.Instance.PlaySound(Clip, Type, Volume);
    }
}
