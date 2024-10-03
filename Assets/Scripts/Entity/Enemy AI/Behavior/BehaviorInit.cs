using BehaviorDesigner.Runtime;
using UnityEngine;

public class BehaviorInit : MonoBehaviour
{
    private Transform player;
    
    private void Start()
    {
        player = GameManager.Instance.Player.transform;
        
    }
}
