using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using Tech.Singleton;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Player { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Player = FindAnyObjectByType<PlayerController>();
        
        SharedTransform tmp = new();
        tmp.SetValue(Player.transform);
        GlobalVariables.Instance.SetVariable(Constant.Target, tmp);
        
        DOTween.Init().SetCapacity(200, 50);
    }
}
