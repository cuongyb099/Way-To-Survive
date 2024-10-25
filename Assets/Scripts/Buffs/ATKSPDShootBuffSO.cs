using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdvanceBuffSO", menuName = "Item/Buff/new AdvanceBuffSO")]
public class ATKSPDShootBuffSO : BasicBuffSO
{
    public int BuffAfter = 3;
    private int dem;
    public override void StartStatus(StatsController controller)
    {
        PlayerEvent.OnShoot += AddSpeed;
    }
    public override void EndStatus(StatsController controller)
    {
        PlayerEvent.OnShoot -= AddSpeed;
    }
    public void AddSpeed()
    {
        dem++;
        Debug.Log(dem);
        if(dem == BuffAfter)
        {
            dem = 0;
            //Controller.AddModifier(StatType, new StatModifier(Value, ModifierType));
        }
    }
}
