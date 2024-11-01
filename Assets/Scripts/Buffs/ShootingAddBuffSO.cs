using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdvanceBuffSO", menuName = "Item/Buff/new AdvanceBuffSO")]
public class ShootingAddBuffSO : BasicBuffSO
{
	public BasicBuffSO BuffSO1;
	public int BuffAfter = 3;

	public override BuffStatusEffect AddStatusEffect(StatsController controller)
	{
		ShootingAddEffect buffEffect = new ShootingAddEffect(this, controller);
		controller.ApplyEffect(buffEffect);
		return buffEffect;
	}
}
