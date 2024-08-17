using System;
using System.Collections.Generic;
using Tech.Observer;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    private Transform player;
    private Grid grid;
    
    [SerializeField] private Material indicatorPlacementMat;
    [SerializeField] private float rangeIndicatorPlace = 3f;
    [SerializeField] private BuildingInventorySO buildingInventory;
    
    private BaseStructure structureSelected;
    private Vector3 curPosToPlace;
    private Dictionary<int,BaseStructure> listPrefabStructure;
    private Transform indicatorHolder;
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        grid = FindObjectOfType<Grid>();
        indicatorHolder = transform.parent.Find("Structure Indicator Holder");
        
        Subject.RegisterListener(EventID.OnBuidingSlotBtnClick, x =>
        {
            MachineIndicatorActive((int)x[0]);
        });
    }
    
    private void Start()
    {
        PlayerInput.Instance.OnBuildingInput += CheckingBuilding;
        InitAllIndicator();
    }

    private void InitAllIndicator()
    {
        listPrefabStructure = new();
        
        buildingInventory.listItem.ForEach(x =>
        {
            var clone = Instantiate(x.Data.Prefab, indicatorHolder);
            clone.SetActive(false);
            listPrefabStructure.Add(x.Data.Id, clone.GetComponent<BaseStructure>());
        });
    }
    
    public void MachineIndicatorActive(int IdStructure)
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
        structureSelected.gameObject.transform.position = curPosToPlace;
    }

    private void Update()
    {
        ResetIndicator();
        
        if (!PlayerInput.Instance.IsInBuildingMode || !structureSelected) return;
        
        IndicatorFollow();
    }

    public void ResetIndicator()
    {
        if(!structureSelected || PlayerInput.Instance.IsInBuildingMode) return;
        structureSelected.gameObject.SetActive(false);
        structureSelected = null;
    }
    
    public void CheckingBuilding()
    {
        var clone = Instantiate(structureSelected.gameObject, curPosToPlace, structureSelected.transform.rotation).GetComponent<MeshRenderer>();
        var structure = clone.GetComponent<BaseStructure>();
        structure.SetIsStructure(structureSelected.DefaultMat);
    }
}
