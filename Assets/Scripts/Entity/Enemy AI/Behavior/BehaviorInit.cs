using BehaviorDesigner.Runtime;
using UnityEngine;

public class BehaviorInit : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private Transform player;
    
    private void Awake()
    {
        SharedTransform tmp = new ();
        tmp.SetValue(player);
        GlobalVariables.Instance.SetVariable(Constant.Target, tmp);
=======
    private Transform player;
    
    private void Start()
    {
        player = GameManager.Instance.Player.transform;
        
>>>>>>> thinhDevelop
    }
}
