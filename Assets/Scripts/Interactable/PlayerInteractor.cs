using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
	public List<IInteractable> Interactables { get; private set; }
    public IInteractable Target { get; private set; } = null;
	public PlayerController Controller { get; private set; }
    private float minDist;
    private int curIndex;
    private void Awake()
	{
        Interactables = new List<IInteractable>();
        Controller = GetComponentInParent<PlayerController>();

		InputEvent.OnInputInteract += InteractWithTarget;
	}
	private void OnDestroy()
	{
		InputEvent.OnInputInteract -= InteractWithTarget;
	}
	private void Update()
	{
        if (Interactables.Count == 0) return;

        minDist = float.MaxValue;
        curIndex = 0;
		for (int i = Interactables.Count-1; i >= 0; i--)
		{
            float dist = (gameObject.transform.position - Interactables[i].ObjTransform.position).magnitude;
            if(dist < minDist)
            {
                minDist = dist;
                curIndex = i;
            }
		}
		SwitchToTarget(curIndex);
	}
	private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IInteractable interactable))
        {
            Interactables.Add(interactable);
            interactable.OnKill += () =>
            {
	            if(interactable == Target) Target = null;
	            Interactables.Remove(interactable);
            };
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
	        if (Target == interactable)
	        {
		        Target?.OnDeselect();
		        Target = null;
	        }
	        interactable.OnKill -= () =>
	        {
		        if(interactable == Target) Target = null;
		        Interactables.Remove(interactable);
	        };
            Interactables.Remove(interactable);
        }
    }
    public void InteractWithTarget()
    {
        if (Interactables.Count == 0) return;
        Target?.Interact(Controller);
	}

    public void SwitchToTarget(int index)
    {
	    if(Target == Interactables[index]) return;
	    Target?.OnDeselect();
	    Target = Interactables[index];
	    Target?.OnSelect();
    }
    
}
