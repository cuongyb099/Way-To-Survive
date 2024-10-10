using BehaviorDesigner.Runtime;
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
        //SharedTransform tmp = new();
        //tmp.SetValue(Player.transform);
        //GlobalVariables.Instance.SetVariable(Constant.Target, tmp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
