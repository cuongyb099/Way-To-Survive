using System.Collections.Generic;
using Tech.Logger;
using UnityEngine;

public class ColliderDetectionCtrl : MonoBehaviour
{
    private Dictionary<ColliderDetectionType, ColliderDetection> colliderDetections = new ();
    private bool _isInit;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if(_isInit) return;
        
        foreach (var colliderDetection in transform.parent.GetComponentsInChildren<ColliderDetection>())
        {
            colliderDetections.Add(colliderDetection.DetectionType, colliderDetection);
        }

        _isInit = true;
    }

    public ColliderDetection GetDetection(ColliderDetectionType type)
    {
        Init();
        if (!colliderDetections.TryGetValue(type, out var detection))
        {
            LogCommon.LogError($"{type} Not Found");
        }

        return detection;
    }
}