using UnityEngine;
<<<<<<< HEAD
=======
using Unity.Mathematics;
>>>>>>> 186ffe6606f86b8f00005ec13f0c029857d056e7
using UnityEngine.Rendering;
using PlayerInput = ResilientCore.PlayerInput;

public class PlacementSystem : MonoBehaviour
{
    private Transform player;
    private Grid grid;
    private Material indicatorPlacementMat;
    [SerializeField] private Material defaultMat;
    [SerializeField] private Transform testMachine;
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
        MachineIndicatorActive();
    }

    public void MachineIndicatorActive()
    {
        testMachine.gameObject.SetActive(true);
        var celltoPlace = grid.WorldToCell(player.position + player.rotation * Vector3.forward * 2f);
        curPosToPlace = grid.GetCellCenterWorld(celltoPlace);
        testMachine.position = curPosToPlace;
    }
    
    public void CheckingBuilding()
    {
        var celltoPlace = grid.WorldToCell(player.position + Vector3Int.right);

<<<<<<< HEAD
        var machine = Instantiate(testMachine.gameObject, curPosToPlace, Quaternion.identity).GetComponent<MeshRenderer>();
=======
        var machine = Instantiate(testMachine.gameObject, curPosToPlace, quaternion.identity).GetComponent<MeshRenderer>();
>>>>>>> 186ffe6606f86b8f00005ec13f0c029857d056e7
        machine.materials = new []{defaultMat};
        machine.shadowCastingMode = ShadowCastingMode.On;
    }
}
