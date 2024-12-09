using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioButtonHelper : MonoBehaviour
{
    public AudioClip Clip;
    public SoundVolumeType Type;
    [Range(0f, 1f)] public float Volume;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Play);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(Play);
    }

    public void Play()
    {
        AudioManager.Instance.PlaySound(Clip,volume: Volume,volumeType: Type);
    }
}
