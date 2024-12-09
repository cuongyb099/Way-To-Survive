using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
    public List<Renderer> Renderers = new ();
    private Dictionary<Renderer, Material[]> _defaultMaterials = new ();
    private Tween[] _doFadeTweens;
    public bool HasFadeMaterial;
    public int MaterialCount { get; private set;}
    
    private void Awake()
    {
        if (Renderers.Count == 0)
        {
            Renderers.AddRange(GetComponentsInChildren<Renderer>());
        }

        foreach (var renderer in Renderers)
        {
            MaterialCount += renderer.materials.Length;
            _defaultMaterials.Add(renderer, renderer.materials);
        }

        _doFadeTweens = new Tween[MaterialCount];
    }


    public void DoFade(float fadeValue, float duration, Stack<Material> poolMaterials, Material materialBase)
    {
        if(HasFadeMaterial) return;

        foreach (var tween in _doFadeTweens)
        {
            if(tween.IsActive()) tween.Kill();
        }
        
        for (int i = 0; i < Renderers.Count; i++)
        {
            Material[] materials = new Material[Renderers[i].materials.Length];
            for (int j = 0; j < Renderers[i].materials.Length; j++)
            {
                if (poolMaterials.Count < 0)
                {
                    materials[j] = Instantiate(materialBase);
                    materials[j].SetTexture("_MainTex", Renderers[i].materials[j].GetTexture("_MainTex")) ;
                    Texture normalMap = Renderers[i].materials[j].GetTexture("_BumpMap");
                    
                    if (!normalMap)
                    {
                        materials[j].SetInt("_UseNormal", 0);
                    }
                    else
                    {
                        materials[j].SetInt("_UseNormal", 1);
                        materials[j].SetTexture("_BumpMap", normalMap);
                    }
                    
                    continue;
                }

                if (poolMaterials.Count > 0)
                {
                    materials[j] = poolMaterials.Pop();
                }
                else
                {
                    return;
                }
                materials[j].SetTexture("_MainTex", Renderers[i].materials[j].GetTexture("_MainTex")) ;
            }
            Renderers[i].materials = materials;
        }

        int index = 0;
        for (int i = 0; i < Renderers.Count; i++)
        {
            for (int j = 0; j < Renderers[i].materials.Length; j++)
            {
                _doFadeTweens[index] = Renderers[i].materials[j].DOFade(fadeValue, duration);
                index++;
            }
        }
        
        HasFadeMaterial = true;
    }

    public void ResetObject(float duration, Stack<Material> poolMaterials, Action onComplete = null)
    {
        if(!HasFadeMaterial) return;
        int i = 0;
        foreach (var renderer in Renderers)
        {
            foreach (var material in renderer.materials)
            {
                if(_doFadeTweens[i].IsActive()) _doFadeTweens[i].Kill();
                _doFadeTweens[i] = material.DOFade(1, duration).OnComplete(() =>
                {
                    poolMaterials.Push(material);
                    renderer.materials = _defaultMaterials[renderer];
                });
                i++;
            }
        }

        HasFadeMaterial = false;
    }
}