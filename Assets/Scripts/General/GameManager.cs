using System;
using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using Tech.Singleton;
using UnityEngine;
using UnityEngine.Audio;

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
    }

    private void Start()
    {
    }
}
