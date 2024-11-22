using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using Tech.Singleton;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int FPSLimitValue = 30;
    public PlayerController Player { get; private set; }
    public AudioSource GMAudioSource { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Player = FindAnyObjectByType<PlayerController>();
        GMAudioSource = GetComponent<AudioSource>();
        Application.targetFrameRate= FPSLimitValue;
        //SharedTransform tmp = new();
        //tmp.SetValue(Player.transform);
        //GlobalVariables.Instance.SetVariable(Constant.Target, tmp);
    }

    public void PlaySound(AudioClip clip)
    {
        GMAudioSource.PlayOneShot(clip);
    }

    public void PlaySoundAtPosition(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position);
    }
}
