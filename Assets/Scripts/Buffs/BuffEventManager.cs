using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Tech.Singleton;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;
using Random = System.Random;

public class BuffEventManager : Singleton<BuffEventManager>
{
    [SerializeField] private ListOfBuffTypeSO positiveBuffsSO;
    [SerializeField] private ListOfBuffTypeSO negativeBuffsSO;
    [SerializeField] private List<BaseBuffSO> positiveBuffListSO;
    [SerializeField] private List<BaseBuffSO> negativeBuffListSO;
    protected override void Awake()
    {
        base.Awake();
        GameEvent.OnChangeTimeOfDay += ChoseTimeOfDayBuff;
    }

    private void OnDestroy()
    {
        GameEvent.OnChangeTimeOfDay -= ChoseTimeOfDayBuff;
    }

    private void ChooseBuffWithRate(int positiveRate)
    {
        if (UnityEngine.Random.value <= positiveRate)
        {
            AddPositiveBuff();
            return;
        }
        AddNegativeBuff();
    }

    private void AddPositiveBuff()
    {
        BaseBuffSO buff = positiveBuffsSO.ChooseRandomBuff();
        positiveBuffListSO.Add(buff);
        buff.AddStatusEffect(GameManager.Instance.Player.GetComponent<StatsController>());
    }
    private void AddNegativeBuff()
    {
        BaseBuffSO buff = negativeBuffsSO.ChooseRandomBuff();
        negativeBuffListSO.Add(buff);
        
        GameEvent.EnemySpawnEvent += x =>
        {
            buff.AddStatusEffect(x.GetComponent<StatsController>());
        };                                                                                                  
    }

    private void RemoveAllBuff()
    {
        for (int i = negativeBuffListSO.Count-1; i>=0; --i)
        {
            GameEvent.EnemySpawnEvent -= x =>
            {
                negativeBuffListSO[i].AddStatusEffect(x.GetComponent<StatsController>());
            };
            negativeBuffListSO.RemoveAt(i);
        }
        for (int i = positiveBuffListSO.Count-1; i>=0; --i)
        {
            GameManager.Instance.Player.GetComponent<StatsController>().RemoveEffect(positiveBuffListSO[i]);
            positiveBuffListSO.RemoveAt(i);
        }
    }
    private void ChoseTimeOfDayBuff(TimeOfTheDay timeOfTheDay)
    {
        RemoveAllBuff();
        switch (timeOfTheDay)
        {
            case TimeOfTheDay.MidNight:
                AddPositiveBuff();
                break;
            case TimeOfTheDay.EarlyMorning:
                AddPositiveBuff();
                break;
            case TimeOfTheDay.Morning:
                AddPositiveBuff();
                break;
            case TimeOfTheDay.Noon:
                AddPositiveBuff();
                break;
            case TimeOfTheDay.Afternoon:
                AddPositiveBuff();
                break;
            case TimeOfTheDay.Evening:
                AddPositiveBuff();
                break;
            default:
                return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
