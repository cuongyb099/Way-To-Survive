using System;
using ProjectDawn.Navigation;
using ProjectDawn.Navigation.Hybrid;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(AgentAuthoring))]
[DisallowMultipleComponent]
public class CustomAgent : MonoBehaviour
{
    [Header("Can't Change In Runtime")]

    private Stat _speed;
    [SerializeField]
    private float _acceleration = 8;

    [SerializeField]
    private float _angularSpeed = 120;

    [SerializeField]
    private float _stoppingDistance = 0;

    [SerializeField]
    private bool _autoBreaking = true;

    public float Speed
    {
        get
        {
            if (_speed == null) return 0;
            return _speed.Value;
        }
        private set
        {
            var data = GetAgentLocomotion();
            data.Speed = value;
            ChangeAgentLocomotion(data);
        }
    }

    public float Acceleration
    {
        get => _acceleration;
        set
        {
            var data = GetAgentLocomotion();
            data.Acceleration = value;
            ChangeAgentLocomotion(data);
        }
    }

    public float AngularSpeed
    {
        get => _angularSpeed;
        set
        {
            var data = GetAgentLocomotion();
            data.AngularSpeed = value;
            ChangeAgentLocomotion(data);
        }
    }

    public float StoppingDistance
    {
        get => _stoppingDistance;
        set
        {
            var data = GetAgentLocomotion();
            data.StoppingDistance = value;
            ChangeAgentLocomotion(data);
        }
    }

    public bool AutoBreaking
    {
        get => _autoBreaking;
        set
        {
            var data = GetAgentLocomotion();
            data.AutoBreaking= value;
            ChangeAgentLocomotion(data);
        }
    }
    
    Entity m_Entity;

    /// <summary>
    /// Returns default component of <see cref="TankLocomotion"/>.
    /// </summary>
    public AgentLocomotion DefaultLocomotion => new()
    {
        Speed = Speed,
        Acceleration = _acceleration,
        AngularSpeed = math.radians(_angularSpeed),
        StoppingDistance = _stoppingDistance,
        AutoBreaking = _autoBreaking,
    };
    
    void Awake()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        m_Entity = GetComponent<AgentAuthoring>().GetOrCreateEntity();
        world.EntityManager.AddComponentData(m_Entity, DefaultLocomotion);

        if (GetComponentInChildren<StatsController>().TryGetStat(StatType.Speed, out _speed))
        {
            Speed = _speed.Value;
            _speed.OnValueChange += HandleSpeedChange;
        }
    }

    private void HandleSpeedChange()
    {
        Speed = _speed.Value;
    }

    private void OnDisable()
    {
        _speed.OnValueChange -= HandleSpeedChange;
    }

    void OnDestroy()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world != null)
            world.EntityManager.RemoveComponent<AgentLocomotion>(m_Entity);
    }

    protected AgentLocomotion GetAgentLocomotion()
    {
        return World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<AgentLocomotion>(m_Entity);
    }

    protected void ChangeAgentLocomotion(AgentLocomotion locomotion)
    {
        var entity = World.DefaultGameObjectInjectionWorld.EntityManager;
        entity.SetComponentData(m_Entity,locomotion);
    }
    
    internal class CustomAgentBaker : Baker<CustomAgent>
    {
        public override void Bake(CustomAgent authoring)
        {
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), authoring.DefaultLocomotion);
        }
    }
}