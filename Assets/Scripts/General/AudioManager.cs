using Tech.Pooling;
using Tech.Singleton;
using UnityEngine;

//Simple Sound Manager 
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private GameObject audioPrefab;
    
    public void PlaySound(AudioClip audioClip, bool isLoop)
    {
        AudioChild audioChild = ObjectPool.Instance.SpawnObject(audioPrefab,
            default, default, PoolType.Audio).GetComponent<AudioChild>();

        AudioSource source = audioChild.Source;
        SoundSetting soundSetting = SoundSetting.GetDefault();
        soundSetting.Loop = isLoop;
        source.clip = audioClip;
        soundSetting.Play(source);
        audioChild.WaitToReturnPool();
    }

    public void PlaySound(AudioClip[] audioClips, bool isLoop)
    {
        AudioClip targetClip = audioClips[Random.Range(0, audioClips.Length)];
        PlaySound(targetClip, isLoop);
    }
    
    public void PlaySound(AudioClip audioClip, SoundSetting setting, Transform parent = null)
    {
        AudioChild audioChild = ObjectPool.Instance.SpawnObject(audioPrefab,
            default, default, PoolType.Audio).GetComponent<AudioChild>();
        audioChild.transform.localPosition = Vector3.zero;

        if (parent == null)
        {
            audioChild.transform.SetParent(parent, false);
        }
        
        AudioSource source = audioChild.Source;
        source.clip = audioClip;
        setting.Play(source);
        audioChild.WaitToReturnPool();
    }

    public void PlaySound(AudioClip[] audioClips, SoundSetting setting, Transform parent = null)
    {
        AudioClip targetClip = audioClips[Random.Range(0, audioClips.Length)];
        PlaySound(targetClip, setting, parent);
    }
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
            Loop = false
        };
    }
}
