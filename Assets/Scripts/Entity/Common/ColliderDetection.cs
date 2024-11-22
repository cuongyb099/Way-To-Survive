using System;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    private Collider col;
    public Action<Collider> CallbackOnTriggerEnter;
    
    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        SetActiveCollider(false);
    }

    public void SetActiveCollider(bool value)
    {
        col.enabled = value;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        CallbackOnTriggerEnter?.Invoke(other);
    }
}
