using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieRagdollAnimation : RagdollAnimationBase
{
    private Animator _animator;
    private Rigidbody[] _ragdollRigidbodies;

    private void Awake()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        if(_animator) return;

        _animator = GetComponent<Animator>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public override void DisableRagdoll()
    {
        LoadComponent();
        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        
        _animator.enabled = true;
    }

    public override void EnableRagdoll()
    {
        LoadComponent();
        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
        
        _animator.enabled = false;
    }
}