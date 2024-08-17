using UnityEngine;

public abstract class ItemBaseSO : ScriptableObject
{
    public int Id => GetInstanceID();
    public Sprite IconUI;
    public string Name;
}