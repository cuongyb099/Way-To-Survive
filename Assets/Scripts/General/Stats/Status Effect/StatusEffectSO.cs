using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Status Effect")]
public class StatusEffectSO : ScriptableObject
{
    public int ID => GetInstanceID();

    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField, TextArea] public string Description { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }
    [field: SerializeField] public bool HasDuration { get; protected set; } = true;
    [field: SerializeField] public float Duration { get; protected set; }
    [field: SerializeField] public bool Stackable { get; protected set; } = true;
    [field: SerializeField, Range(0, 999)] public int MaxStack { get; protected set; } = 64;
    [field: SerializeField] public StatusEffectType EffectType { get; protected set; }
    
    [field: Header("MultiThreading Update")]
    [field: SerializeField] public bool UseAdvanceUpdate { get; protected set; } = true;
    public virtual void StartStatus(StatsController controller)
    {

    }
    public virtual void UpdateStatus(StatsController controller)
    {

    }
    public virtual void EndStatus(StatsController controller)
    {

    }
}