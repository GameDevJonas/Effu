// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Land"",
            ""id"": ""8c49f43b-d926-4381-956f-92c830968837"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0cd42645-9b4c-413d-bd95-2e80876d3c63"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""28b82b49-179c-40fc-b475-1dcdaee2bde7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Call"",
                    ""type"": ""Button"",
                    ""id"": ""9a39ae42-d37a-443c-92ea-2a3652799304"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""e056dbdd-e459-487e-abb1-b39d5865c888"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Sideways"",
                    ""id"": ""fcb7e44f-172c-4aaf-91f4-1d13a2b7f0ef"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""dafed31a-562c-4c25-9a32-01d56e138d5a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8b4b3428-ae03-4fba-93bc-9cd4274acafc"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8f642518-4b59-46b7-8ff8-26ba4636b4af"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""807f5c23-ac84-42fd-ac0e-9be8d6df50b0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00f62d00-35a5-4c6a-88ae-11638d3b9e5b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99adfd21-12a3-44ea-8d9c-2d734890308d"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controls"",
                    ""action"": ""Call"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3373e0f-8244-4b33-8e5d-1758c3451aa4"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controls"",
                    ""action"": ""Call"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2897f468-9c81-48d1-a974-efc3ffe8d1d2"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controls"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""043c2300-3748-4785-af19-8c788c74ce0e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controls"",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controls"",
            ""bindingGroup"": ""Controls"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Land
        m_Land = asset.FindActionMap("Land", throwIfNotFound: true);
        m_Land_Move = m_Land.FindAction("Move", throwIfNotFound: true);
        m_Land_Jump = m_Land.FindAction("Jump", throwIfNotFound: true);
        m_Land_Call = m_Land.FindAction("Call", throwIfNotFound: true);
        m_Land_Grab = m_Land.FindAction("Grab", throwIfNotFound: true);
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

    // Land
    private readonly InputActionMap m_Land;
    private ILandActions m_LandActionsCallbackInterface;
    private readonly InputAction m_Land_Move;
    private readonly InputAction m_Land_Jump;
    private readonly InputAction m_Land_Call;
    private readonly InputAction m_Land_Grab;
    public struct LandActions
    {
        private @PlayerControls m_Wrapper;
        public LandActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Land_Move;
        public InputAction @Jump => m_Wrapper.m_Land_Jump;
        public InputAction @Call => m_Wrapper.m_Land_Call;
        public InputAction @Grab => m_Wrapper.m_Land_Grab;
        public InputActionMap Get() { return m_Wrapper.m_Land; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LandActions set) { return set.Get(); }
        public void SetCallbacks(ILandActions instance)
        {
            if (m_Wrapper.m_LandActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnJump;
                @Call.started -= m_Wrapper.m_LandActionsCallbackInterface.OnCall;
                @Call.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnCall;
                @Call.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnCall;
                @Grab.started -= m_Wrapper.m_LandActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnGrab;
            }
            m_Wrapper.m_LandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Call.started += instance.OnCall;
                @Call.performed += instance.OnCall;
                @Call.canceled += instance.OnCall;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
            }
        }
    }
    public LandActions @Land => new LandActions(this);
    private int m_ControlsSchemeIndex = -1;
    public InputControlScheme ControlsScheme
    {
        get
        {
            if (m_ControlsSchemeIndex == -1) m_ControlsSchemeIndex = asset.FindControlSchemeIndex("Controls");
            return asset.controlSchemes[m_ControlsSchemeIndex];
        }
    }
    public interface ILandActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCall(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
    }
}
