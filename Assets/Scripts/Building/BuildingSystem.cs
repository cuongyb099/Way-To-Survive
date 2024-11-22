using System.Collections.Generic;
using Tech.Singleton;
using UnityEngine;

public class BuildingSystem : Singleton<BuildingSystem>
{
    private Transform player;
    private Grid grid;
    
    [SerializeField] private Material indicatorPlacementMat;
    [SerializeField] private float rangeIndicatorPlace = 3f;

    public BuildingInventorySO Inventory;
    [HideInInspector] public int ObstaclesOccupy;

    private Color greenIndicator = new Color(0.13f, 1, 0 ,0.09f);
    private Color redIndicator = new Color(1, 0, 0 ,0.09f);
    
    private BaseStructure structureSelected;
    private Vector3 curPosToPlace;
    private Dictionary<int,BaseStructure> listPrefabStructure;
    private Transform indicatorHolder;
    
    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<PlayerController>().transform;
        grid = FindObjectOfType<Grid>();
        indicatorHolder = transform.parent.Find("Structure Indicator Holder");

        InputEvent.OnBuilding += Build;
        InputEvent.OnRotateStructure += RotateIndicator;

        InitAllIndicator();
    }

	private void OnDestroy()
	{
		InputEvent.OnBuilding -= Build;
		InputEvent.OnRotateStructure -= RotateIndicator;
	}

	private void InitAllIndicator()
    {
        listPrefabStructure = new();
        
        Inventory.listItem.ForEach(x =>
        {
            var clone = Instantiate(x.Data.Prefab, indicatorHolder);
            clone.SetActive(false);
            clone.AddComponent<Rigidbody>().isKinematic = true;
            listPrefabStructure.Add(x.Data.Id, clone.GetComponent<BaseStructure>());
        });
    }
    
    public void ActiveIndicator(int IdStructure)
    {
        if(structureSelected) structureSelected.gameObject.SetActive(false);
        structureSelected = listPrefabStructure[IdStructure];
        if(!structureSelected) return;
        structureSelected.gameObject.SetActive(true);
        structureSelected.SetIsIndicator(indicatorPlacementMat);
        
        IndicatorFollow();
    }

    private void IndicatorFollow()
    {
        var celltoPlace = grid.WorldToCell(player.position + player.rotation * Vector3.forward * rangeIndicatorPlace);
        curPosToPlace = grid.GetCellCenterWorld(celltoPlace);
        curPosToPlace.y = structureSelected.transform.position.y;
        structureSelected.transform.position = curPosToPlace;

        indicatorPlacementMat.color = ObstaclesOccupy > 0 ? redIndicator : greenIndicator;
    }

    private void Update()
    {
        ResetIndicator();
        
        if (!PlayerInput.Instance.IsInBuildingMode || !structureSelected) return;
        
        IndicatorFollow();
    }

    public void RotateIndicator()
    {
        if(!PlayerInput.Instance.IsInBuildingMode) return;
        
        structureSelected.transform.Rotate(Vector3.up, 90);
    }
    
    public void ResetIndicator()
    {
        if(!structureSelected || PlayerInput.Instance.IsInBuildingMode) return;
        structureSelected.gameObject.SetActive(false);
        structureSelected = null;
        ObstaclesOccupy = 0;
    }
    
    public void Build()
    {
        if (ObstaclesOccupy > 0)
        {
            
            return;
        }
            
        var clone = Instantiate(structureSelected.gameObject, curPosToPlace, structureSelected.transform.rotation);
        var structure = clone.GetComponentInChildren<BaseStructure>();
        structure.SetIsStructure(structureSelected.DefaultMat);
    }
}
