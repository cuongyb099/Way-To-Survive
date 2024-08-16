using System;
using Tech.Logger;
using Tech.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ResilientCore
{
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
			AddListeners();
		}

		private void Start()
		{
			InputActions.Enable();
			
			InputActions.BasicAction.BuidingMode.started += ctx =>
			{
				IsInBuildingMode = !IsInBuildingMode;
			};

			InputActions.BasicAction.Buiding.started += ctx =>
			{
				OnBuildingInput?.Invoke();
			};
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

		}
		private void RemoveListeners()
		{

		}
		//Methods
		private Vector3 GetMovementInput()
		{
			Vector2 v = PlayerControlActions.Movement.ReadValue<Vector2>();

            return new Vector3(v.x,0,v.y);
		}
	}
}
