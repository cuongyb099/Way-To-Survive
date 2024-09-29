using System.Collections;
using System.Collections.Generic;
using Tech.Singleton;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Player { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Player = FindAnyObjectByType<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
