using Tech.Singleton;
using UnityEngine;

namespace ResilientCore
{
	public class PlayerInput : Singleton<PlayerInput>
	{

		public PlayerControls InputActions { get; private set; }
		public PlayerControls.BasicActionActions PlayerControlActions { get; private set; }
		//Input
		public Vector2 MovementInput { get { return GetMovementInput(); } }

		public bool IsWalkInput
		{
			get
			{
				return walking && EnableWalk;
			}
		}
		public bool IsSprintInput
		{
			get
			{
				return sprinting && EnableSprint;
			}
		}
		public bool IsAttackInput
		{
			get
			{
				return attaking && EnableAttack;
			}
		}
		public bool IsRollInput { get; private set; }
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
		private Vector2 GetMovementInput()
		{
			return PlayerControlActions.Movement.ReadValue<Vector2>();
		}
	}
}
