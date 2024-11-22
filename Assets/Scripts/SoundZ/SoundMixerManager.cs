using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public void SetMasterVolume(float volume)
    {
        Mixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20f);
    }
    public void SetGameVolume(float volume)
    {
        Mixer.SetFloat("GameVolume", Mathf.Log10(volume) * 20f);

    }
    public void SetMusicVolume(float volume)
    {
        Mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);

    }
    public void SetUIVolume(float volume)
    {
        Mixer.SetFloat("UIVolume", Mathf.Log10(volume) * 20f);

    }
}
