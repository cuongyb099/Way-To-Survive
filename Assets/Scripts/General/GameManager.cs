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
    }
}
