using BehaviorDesigner.Runtime;
using UnityEngine;

public class BehaviorInit : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private void Awake()
    {
        SharedTransform tmp = new ();
        tmp.SetValue(player);
        GlobalVariables.Instance.SetVariable(Constant.Target, tmp);
    }
}
