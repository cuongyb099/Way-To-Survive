using System;
using Tech.Pooling;
using Tech.Singleton;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

//Simple Sound Manager 
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private GameObject _audioPrefab;
    [SerializeField] private AudioClip[] _startedBGMusic;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioMixerGroup _masterGroup;
    [SerializeField] private AudioMixerGroup _bgGroup;
    [SerializeField] private AudioMixerGroup _fxGroup;
    
    private static readonly string _masterVolume = "Master Volume"; 
    private static readonly string _bgVolume = "BG Volume"; 
    private static readonly string _fxVolume = "FX Volume"; 
    
    private void Start()
    {
        LoadSave();
        PlayStartSound();
    }

    private void LoadSave()
    {
        SetVolume(_masterVolume, ES3.Load(_masterVolume, 1f));
        SetVolume(_bgVolume, ES3.Load(_bgVolume, 1f));
        SetVolume(_fxVolume, ES3.Load(_fxVolume, 1f));
    }

    private void PlayStartSound()
    {
        if(_startedBGMusic.Length == 0) return;
        
        foreach (AudioClip music in _startedBGMusic)
        {
            PlaySound(music, true);
        }
    }

    public void PlaySound(AudioClip audioClip, bool isLoop, SoundVolumeType volumeType = SoundVolumeType.SOUNDFX_VOLUME)
    {
        AudioChild audioChild = ObjectPool.Instance.SpawnObject(_audioPrefab,
            default, default, PoolType.Audio).GetComponent<AudioChild>();

        AudioSource source = audioChild.Source;
        SoundSetting soundSetting = SoundSetting.GetDefault();
        soundSetting.Loop = isLoop;
        source.clip = audioClip;
        source.outputAudioMixerGroup = GetMixerGroup(volumeType);
        soundSetting.Play(source);
        audioChild.WaitToReturnPool();
    }

    public void PlaySound(AudioClip[] audioClips, bool isLoop, SoundVolumeType volumeType = SoundVolumeType.SOUNDFX_VOLUME)
    {
        AudioClip targetClip = audioClips[Random.Range(0, audioClips.Length)];
        PlaySound(targetClip, isLoop, volumeType);
    }
    
    public void PlaySound(AudioClip audioClip, SoundSetting setting, Transform parent = null)
    {
        AudioChild audioChild = ObjectPool.Instance.SpawnObject(_audioPrefab,
            default, default, PoolType.Audio).GetComponent<AudioChild>();
        audioChild.transform.localPosition = Vector3.zero;

        if (parent)
        {
            audioChild.transform.SetParent(parent, false);
        }
        
        AudioSource source = audioChild.Source;
        source.clip = audioClip;
        source.outputAudioMixerGroup = GetMixerGroup(setting.VolumeType);
        setting.Play(source);
        audioChild.WaitToReturnPool();
    }

    public void PlaySound(AudioClip[] audioClips, SoundSetting setting, Transform parent = null)
    {
        AudioClip targetClip = audioClips[Random.Range(0, audioClips.Length)];
        PlaySound(targetClip, setting, parent);
    }

    private AudioMixerGroup GetMixerGroup(SoundVolumeType volumeType)
    {
        switch (volumeType)
        {
            case SoundVolumeType.MASTER_VOLUME:
                return _masterGroup;
            case SoundVolumeType.BGM_VOLUME:
                return _bgGroup;
            case SoundVolumeType.SOUNDFX_VOLUME:
                return _fxGroup;
            default:
                return null;
        }
    }
    
    private void SetVolume(string parameter, float value)
    {
        value = (float)Math.Clamp(value, 0.0001, 1);
        _mixer.SetFloat(parameter, (float)Math.Log10(value) * 20f);
    }
}

public enum SoundVolumeType
{
    MASTER_VOLUME,
    BGM_VOLUME,
    SOUNDFX_VOLUME
}

public struct SoundSetting
{
    public float Volume;
    public float Pitch;
    public float StereoPan;
    public float SpatialBlend;
    public float MinDistance;
    public float MaxDistance;
    public bool Loop;
    public AudioRolloffMode RolloffMode;
    public SoundVolumeType VolumeType;
    
    public void Play(AudioSource source)
    {
        source.volume = Volume;
        source.pitch = Pitch;
        source.panStereo = StereoPan;
        source.spatialBlend = SpatialBlend;
        source.loop = Loop;
        source.minDistance = MinDistance;
        source.maxDistance = MaxDistance;
        source.rolloffMode = RolloffMode;
        source.Play();
    }

    public static SoundSetting GetDefault()
    {
        return new SoundSetting()
        {
            Volume = 1,
            Pitch = 1,
            StereoPan = 0,
            SpatialBlend = 0,
            Loop = false,
            VolumeType = SoundVolumeType.SOUNDFX_VOLUME
        };
    }
}
