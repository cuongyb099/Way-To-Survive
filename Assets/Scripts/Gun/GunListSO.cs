using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunListSO",menuName ="Item/Gun/new GunListSO")]
public class GunListSO : ScriptableObject
{
    public List<GunBase> Guns;
}
