using UnityEngine;

[CreateAssetMenu(fileName = "StructureData", menuName = "Building/StructureData")]
public class StructureDataSO : ItemBaseSO
{
    public GameObject Prefab;
    public int MaxHP;
    public float Damage;
}