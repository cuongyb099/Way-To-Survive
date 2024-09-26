using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseStructure : MonoBehaviour, IDamagable
{
    public float HP { get; set; }

    private MeshRenderer renderer;
    private Collider collider;
    public Material DefaultMat { get; private set; }
    public Action OnDamaged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Action IDamagable.OnDamaged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Action IDamagable.OnDeath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    protected virtual void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();

        DefaultMat = renderer.materials.FirstOrDefault();
    }

    //Unity only allow change array of materials meshRenderer 
    public virtual void SetIsIndicator(Material matIndicator)
    {
        renderer.materials = new[] { matIndicator };
        renderer.shadowCastingMode = ShadowCastingMode.Off;
        collider.enabled = false;
    }

    public virtual void SetIsStructure()
    {
        renderer.materials = new[] { DefaultMat };
        renderer.shadowCastingMode = ShadowCastingMode.On;
        collider.enabled = true;
    }
    
    public virtual void SetIsStructure(Material Mat)
    {
        renderer.materials = new[] { Mat };
        renderer.shadowCastingMode = ShadowCastingMode.On;
        collider.enabled = true;
    }
    
    public virtual void Damage(DamageInfo info)
    {
        HP -= info.Damage;
        if (HP <= 0) HP = 0;
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    void IDamagable.Damage(DamageInfo info)
    {
        throw new NotImplementedException();
    }

    void IDamagable.Death()
    {
        throw new NotImplementedException();
    }
}
