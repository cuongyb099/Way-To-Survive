using System;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    private Collider _collider;
    public Action<Collider> CallbackColliderEnter;
    
    [field: SerializeField] public ColliderDetectionType DetectionType {get; private set;}
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void Start()
    {
        SetActiveCollider(false);
    }

    public void SetActiveCollider(bool value)
    {
        _collider.enabled = value;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        CallbackColliderEnter?.Invoke(other);
    }
}
