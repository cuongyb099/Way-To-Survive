using UnityEngine;

public class ZombieRagdollAnimation : RagdollAnimationBase
{
    [SerializeField]
    private Animator _animator;
    private Rigidbody[] _ragdollRigidbodies;
    private Rigidbody _spineRigidbody;
    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        if (!_animator)
        {
            _animator = GetComponent<Animator>();
        }

        _spineRigidbody = _animator.GetBoneTransform(HumanBodyBones.Spine).GetComponent<Rigidbody>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public override void DisableRagdoll()
    {
        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        
        _animator.enabled = true;
    }

    public override void EnableRagdoll()
    {
        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
        
        _animator.enabled = false;
    }
}