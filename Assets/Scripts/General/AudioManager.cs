using System.Collections.Generic;
using Tech.Logger;
using Tech.Pooling;
using Tech.Singleton;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{
    private AudioSource audioSource;

    [SerializeField] private GameObject prefabAudioChild;
    [SerializeField] private AudioStorageSO audioStorage;

    private List<AudioSource> audioSources = new();
    
    private bool isSound;
    
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name, bool isLoop)
    {
        if (!audioStorage.ContainAudio(name))
        {
            LogCommon.LogError("Sound Not Found");
            return;
        }
        
        AudioSource source = ObjectPool.Instance.SpawnObject(prefabAudioChild,
            Vector3.zero, Quaternion.identity).GetComponent<AudioSource>();
        source.gameObject.SetActive(false);
        source.clip = audioStorage.GetAudio(name);
        source.loop = isLoop;
        
        if(!audioSources.Contains(source)) audioSources.Add(source);
        
        source.gameObject.SetActive(true);
    }

    public void SetSound(bool value)
    {
        isSound = value;
        if (!isSound) audioSources.ForEach(x => x.Stop());
    }
}
