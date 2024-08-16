using UnityEngine;


public enum StructureType
{
    DmgDealer,
    Obstacle,
    Buff
}

[CreateAssetMenu(fileName = "StructureSO", menuName = "SO/structureSO")]
public class StructureSO : ScriptableObject
{
    public float MaxHp;
    public float Damage;
    public StructureType Type;
}