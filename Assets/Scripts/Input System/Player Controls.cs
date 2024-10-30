//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input System/Player Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""BasicAction"",
            ""id"": ""5444ec47-6749-4336-99ac-29534346ffa1"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2022de74-01f2-408d-9a56-810b042e19b8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""75fdb3ef-2697-45ed-9159-a1c5279fa76e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""BuidingMode"",
                    ""type"": ""Button"",
                    ""id"": ""192dbd70-c23a-47d4-be4a-c5cea0ab92a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Buiding"",
                    ""type"": ""Button"",
                    ""id"": ""90ea0459-4247-4033-9ff4-daec154bab5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchGuns"",
                    ""type"": ""Button"",
                    ""id"": ""dc97664a-34f1-4b4a-86de-a0c79b3ef6c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerRotate"",
                    ""type"": ""Value"",
                    ""id"": ""74597e96-d7c1-4953-8288-f6843fdcf775"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""42013816-30bf-4da3-a05a-ded2cd042162"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootStick"",
                    ""type"": ""Value"",
                    ""id"": ""eb05b435-0d87-49ed-bc03-59d464ee8a85"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""752e2304-83ec-45e1-a632-e7956241a486"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""8d633712-893b-4194-bd75-f7fc174aac95"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b8647e2c-0b59-462b-8e19-4bd088d95554"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e3ca137c-20ac-4ac7-9642-bbbd4e5414da"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""02aba5c7-db0f-44a3-8e67-183c6ec6a100"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""04d93006-2fde-4e75-a38d-ab562ed44a41"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""46fe3d10-cb76-49a9-aeb4-a0df25a33957"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""044a9dc5-387c-493d-94be-f6b43c0277d9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29617d57-dbb4-491f-b327-9f5721c4c6a0"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b869f076-c9f9-49e3-a215-b3fd028a1391"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BuidingMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07b803ad-691d-47b8-a11f-c1ab04d548bf"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Buiding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cfaffca-514b-4566-a243-4cf3916d4242"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchGuns"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80a21415-b486-42c7-a809-0f2eb46eb55c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchGuns"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8ebe369-7272-48fc-baf6-12ef0bd49275"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30f3a804-ca3a-40d0-89c5-50b746bcb667"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6082a4b-b643-47c7-ab59-b6b4f32b4fe1"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""038a0d30-2a36-45b1-827b-f1ad80580dae"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb49533a-3107-4fed-8439-14df55d01b05"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdbfa880-5c02-4c10-8fba-288bf79acb89"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9ac972f-d4d8-4379-be4d-43596eb56710"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BasicAction
        m_BasicAction = asset.FindActionMap("BasicAction", throwIfNotFound: true);
        m_BasicAction_Movement = m_BasicAction.FindAction("Movement", throwIfNotFound: true);
        m_BasicAction_Rotate = m_BasicAction.FindAction("Rotate", throwIfNotFound: true);
        m_BasicAction_BuidingMode = m_BasicAction.FindAction("BuidingMode", throwIfNotFound: true);
        m_BasicAction_Buiding = m_BasicAction.FindAction("Buiding", throwIfNotFound: true);
        m_BasicAction_SwitchGuns = m_BasicAction.FindAction("SwitchGuns", throwIfNotFound: true);
        m_BasicAction_PlayerRotate = m_BasicAction.FindAction("PlayerRotate", throwIfNotFound: true);
        m_BasicAction_Shoot = m_BasicAction.FindAction("Shoot", throwIfNotFound: true);
        m_BasicAction_ShootStick = m_BasicAction.FindAction("ShootStick", throwIfNotFound: true);
        m_BasicAction_Reload = m_BasicAction.FindAction("Reload", throwIfNotFound: true);
        m_BasicAction_Zoom = m_BasicAction.FindAction("Zoom", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // BasicAction
    private readonly InputActionMap m_BasicAction;
    private List<IBasicActionActions> m_BasicActionActionsCallbackInterfaces = new List<IBasicActionActions>();
    private readonly InputAction m_BasicAction_Movement;
    private readonly InputAction m_BasicAction_Rotate;
    private readonly InputAction m_BasicAction_BuidingMode;
    private readonly InputAction m_BasicAction_Buiding;
    private readonly InputAction m_BasicAction_SwitchGuns;
    private readonly InputAction m_BasicAction_PlayerRotate;
    private readonly InputAction m_BasicAction_Shoot;
    private readonly InputAction m_BasicAction_ShootStick;
    private readonly InputAction m_BasicAction_Reload;
    private readonly InputAction m_BasicAction_Zoom;
    public struct BasicActionActions
    {
        private @PlayerControls m_Wrapper;
        public BasicActionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_BasicAction_Movement;
        public InputAction @Rotate => m_Wrapper.m_BasicAction_Rotate;
        public InputAction @BuidingMode => m_Wrapper.m_BasicAction_BuidingMode;
        public InputAction @Buiding => m_Wrapper.m_BasicAction_Buiding;
        public InputAction @SwitchGuns => m_Wrapper.m_BasicAction_SwitchGuns;
        public InputAction @PlayerRotate => m_Wrapper.m_BasicAction_PlayerRotate;
        public InputAction @Shoot => m_Wrapper.m_BasicAction_Shoot;
        public InputAction @ShootStick => m_Wrapper.m_BasicAction_ShootStick;
        public InputAction @Reload => m_Wrapper.m_BasicAction_Reload;
        public InputAction @Zoom => m_Wrapper.m_BasicAction_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_BasicAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicActionActions set) { return set.Get(); }
        public void AddCallbacks(IBasicActionActions instance)
        {
            if (instance == null || m_Wrapper.m_BasicActionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BasicActionActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Rotate.started += instance.OnRotate;
            @Rotate.performed += instance.OnRotate;
            @Rotate.canceled += instance.OnRotate;
            @BuidingMode.started += instance.OnBuidingMode;
            @BuidingMode.performed += instance.OnBuidingMode;
            @BuidingMode.canceled += instance.OnBuidingMode;
            @Buiding.started += instance.OnBuiding;
            @Buiding.performed += instance.OnBuiding;
            @Buiding.canceled += instance.OnBuiding;
            @SwitchGuns.started += instance.OnSwitchGuns;
            @SwitchGuns.performed += instance.OnSwitchGuns;
            @SwitchGuns.canceled += instance.OnSwitchGuns;
            @PlayerRotate.started += instance.OnPlayerRotate;
            @PlayerRotate.performed += instance.OnPlayerRotate;
            @PlayerRotate.canceled += instance.OnPlayerRotate;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @ShootStick.started += instance.OnShootStick;
            @ShootStick.performed += instance.OnShootStick;
            @ShootStick.canceled += instance.OnShootStick;
            @Reload.started += instance.OnReload;
            @Reload.performed += instance.OnReload;
            @Reload.canceled += instance.OnReload;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
        }

        private void UnregisterCallbacks(IBasicActionActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Rotate.started -= instance.OnRotate;
            @Rotate.performed -= instance.OnRotate;
            @Rotate.canceled -= instance.OnRotate;
            @BuidingMode.started -= instance.OnBuidingMode;
            @BuidingMode.performed -= instance.OnBuidingMode;
            @BuidingMode.canceled -= instance.OnBuidingMode;
            @Buiding.started -= instance.OnBuiding;
            @Buiding.performed -= instance.OnBuiding;
            @Buiding.canceled -= instance.OnBuiding;
            @SwitchGuns.started -= instance.OnSwitchGuns;
            @SwitchGuns.performed -= instance.OnSwitchGuns;
            @SwitchGuns.canceled -= instance.OnSwitchGuns;
            @PlayerRotate.started -= instance.OnPlayerRotate;
            @PlayerRotate.performed -= instance.OnPlayerRotate;
            @PlayerRotate.canceled -= instance.OnPlayerRotate;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @ShootStick.started -= instance.OnShootStick;
            @ShootStick.performed -= instance.OnShootStick;
            @ShootStick.canceled -= instance.OnShootStick;
            @Reload.started -= instance.OnReload;
            @Reload.performed -= instance.OnReload;
            @Reload.canceled -= instance.OnReload;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
        }

        public void RemoveCallbacks(IBasicActionActions instance)
        {
            if (m_Wrapper.m_BasicActionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBasicActionActions instance)
        {
            foreach (var item in m_Wrapper.m_BasicActionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BasicActionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BasicActionActions @BasicAction => new BasicActionActions(this);
    public interface IBasicActionActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnBuidingMode(InputAction.CallbackContext context);
        void OnBuiding(InputAction.CallbackContext context);
        void OnSwitchGuns(InputAction.CallbackContext context);
        void OnPlayerRotate(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnShootStick(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
}
