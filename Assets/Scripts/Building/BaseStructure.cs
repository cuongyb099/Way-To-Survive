using System;
using UnityEngine;
using UnityEngine.Rendering;

public class BaseStructure : MonoBehaviour
{
    private MeshRenderer renderer;
    private MeshCollider collider;
    private Material defaultMat;
    
    [SerializeField] private StructureSO structureData;
    [SerializeField] private float curHp;    

    
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<MeshCollider>();
        
        defaultMat = renderer.materials[0];
    }

    private void OnEnable()
    {
        curHp = structureData.MaxHp;
    }

    public void SetIsIndicatorStructure(Material matIndicator)
    {
        collider.enabled = false;
        renderer.materials = new Material[] { matIndicator };
        renderer.shadowCastingMode = ShadowCastingMode.Off;
    }
}
