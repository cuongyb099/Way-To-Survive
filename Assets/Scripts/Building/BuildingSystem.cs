using System.Collections.Generic;
using Tech.Singleton;
using UnityEngine;

public class BuildingSystem : Singleton<BuildingSystem>
{
    private Transform _player;
    [SerializeField] private Material _indicatorPlacementMat;
    [SerializeField] private float _rangeIndicatorPlace = 3f;

    private static readonly Color greenIndicator = new (0.13f, 1, 0 ,0.09f);
    private static readonly Color redIndicator = new (1, 0, 0 ,0.09f);
    
    private Vector3 curPosToPlace;

    protected override void Awake()
    {
        base.Awake();
        _player = FindObjectOfType<PlayerController>().transform;
		
        InputEvent.OnBuilding += Build;
        InputEvent.OnRotateStructure += RotateIndicator;
    }
    private void OnDestroy()
	{
		InputEvent.OnBuilding -= Build;
		InputEvent.OnRotateStructure -= RotateIndicator;
	}
    private void RotateIndicator()
    {
	    
    }

    private void Build()
    {
	    
    }
}
