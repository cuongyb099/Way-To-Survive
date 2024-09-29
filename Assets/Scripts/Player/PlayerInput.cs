using System;
using Tech.Singleton;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
	public PlayerControls InputActions { get; private set; }
	public PlayerControls.BasicActionActions PlayerControlActions { get; private set; }
	//Input
	public Vector3 MovementInput => GetMovementInput();
	public Vector3 RotationInput => PlayerControlActions.Rotate.ReadValue<Vector2>();

	public bool IsWalkInput => walking && EnableWalk;
	public bool IsSprintInput => sprinting && EnableSprint;
	public bool IsAttackInput => attaking && EnableAttack;
	
	public bool IsInBuildingMode { get; private set; }
	public Action OnBuildingInput;
	public Action OnSwitchGuns;
	
	public bool EnableAttack { get; set; } = true;
	public bool EnableSkills { get; set; } = true;
	public bool EnableWalk { get; set; } = true;
	public bool EnableSprint { get; set; } = true;

	private bool attaking = false;
	private bool walking = false;
	private bool sprinting = false;

	protected override void Awake()
	{
		base.Awake();
		InputActions = new PlayerControls();
		PlayerControlActions = InputActions.BasicAction;
        InputActions.Enable();
		AddListeners();
    }

	private void OnDestroy()
	{
		RemoveListeners();
	}

	private void OnEnable()
	{
		InputActions.Enable();
    }
	private void OnDisable()
	{
		InputActions.Disable();
	}
	private void AddListeners()
	{
        InputActions.BasicAction.BuidingMode.started += ctx =>
        {
            IsInBuildingMode = !IsInBuildingMode;
        };

        InputActions.BasicAction.Buiding.started += ctx =>
        {
            OnBuildingInput?.Invoke();
        };

        InputActions.BasicAction.SwitchGuns.started += ctx =>
        {
            OnSwitchGuns?.Invoke();
        };
    }
	private void RemoveListeners()
	{
        InputActions.BasicAction.BuidingMode.started -= ctx =>
        {
            IsInBuildingMode = !IsInBuildingMode;
        };

        InputActions.BasicAction.Buiding.started -= ctx =>
        {
            OnBuildingInput?.Invoke();
        };

        InputActions.BasicAction.SwitchGuns.started -= ctx =>
        {
            OnSwitchGuns?.Invoke();
        };
    }
	//Methods
	private Vector3 GetMovementInput()
	{
		Vector2 v = PlayerControlActions.Movement.ReadValue<Vector2>();

        return new Vector3(v.x,0,v.y);
	}
	//Methods
	private Vector3 GetMovementInput()
	{
		Vector2 v = PlayerControlActions.Movement.ReadValue<Vector2>();

        return new Vector3(v.x,0,v.y);
	}
}
