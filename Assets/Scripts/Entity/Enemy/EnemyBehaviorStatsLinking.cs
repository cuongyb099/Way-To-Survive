using System.Collections;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class EnemyBehaviorStatsLinking
{
    private static readonly int ATKSpeed = Animator.StringToHash("Atk Speed");
    private static readonly string ATK = "Atk";
    private EnemyCtrl _enemyCtrl;
    private Stat atkStat;
    private Stat atkSpeedStat;
    private Stat speedStat;
    private BehaviorTree behaviorTree;
    private Animator animator;
    private AgentForRootmotion agent;
    
    public EnemyBehaviorStatsLinking(EnemyCtrl enemyCtrl)
    {
        _enemyCtrl = enemyCtrl;
        _enemyCtrl.StartCoroutine(WaitInit());
    }
    private IEnumerator WaitInit()
    {
        yield return new WaitForEndOfFrame();
        behaviorTree = _enemyCtrl.BTree;
        animator = _enemyCtrl.Anim;
        agent = _enemyCtrl.AgentRootmotion;
        var statsCtrl = _enemyCtrl.Stats;
        
        atkStat = statsCtrl.GetStat(StatType.ATK);
        atkSpeedStat = statsCtrl.GetStat(StatType.ATKSpeed);
        speedStat = statsCtrl.GetStat(StatType.Speed);
        
        //ATK
        behaviorTree.SetVariableValue(ATK, atkStat.Value);
        atkStat.OnValueChange += () =>
        {
            behaviorTree.SetVariableValue(ATK, atkStat.Value);
        };

        //ATK Speed
        animator.SetFloat(ATKSpeed, atkSpeedStat.Value);
        atkSpeedStat.OnValueChange += () =>
        {
            animator.SetFloat(ATKSpeed, atkSpeedStat.Value);
        };
        
        //Move Speed
        speedStat.OnValueChange += () =>
        {
            agent.Speed = speedStat.Value;
        };
    }
}