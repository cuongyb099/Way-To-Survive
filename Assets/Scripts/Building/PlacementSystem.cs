using Tech.Logger;
using UnityEngine;
using UnityEngine.Rendering;
using PlayerInput = ResilientCore.PlayerInput;

public class PlacementSystem : MonoBehaviour
{
    private Transform player;
    private Grid grid;
    private Material indicatorPlacementMat;
    [SerializeField] private Material defaultMat;
    [SerializeField] private Transform testMachine;
    [SerializeField] private float rangeIndicator = 2f;
    private Vector3 curPosToPlace;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
        grid = FindObjectOfType<Grid>();

        indicatorPlacementMat = Utilities.LoadResource<Material>("Mat/Object Alpha");
    }
    
    private void Start()
    {
        PlayerInput.Instance.OnBuildingInput += CheckingBuilding;
    }

    private void Update()
    {
        if (!PlayerInput.Instance.IsInBuildingMode) return;
        StructureIndicatorActive();
    }

    public void StructureIndicatorActive()
    {
        testMachine.gameObject.SetActive(true);
        var celltoPlace = grid.WorldToCell(player.position + player.rotation * Vector3.forward * rangeIndicator);
        curPosToPlace = grid.GetCellCenterWorld(celltoPlace);
        testMachine.position = curPosToPlace;
        
    }
    
    public void CheckingBuilding()
    {
        var celltoPlace = grid.WorldToCell(player.position + Vector3Int.right);

        var machine = Instantiate(testMachine.gameObject, curPosToPlace, Quaternion.identity).GetComponent<MeshRenderer>();
        machine.materials = new []{defaultMat};
        machine.shadowCastingMode = ShadowCastingMode.On;
    }
}
