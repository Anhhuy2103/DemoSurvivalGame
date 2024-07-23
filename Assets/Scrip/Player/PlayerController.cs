//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scrip/Player/PlayerController.inputactions
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

public partial class @PlayerController: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1baa413e-9155-462f-a7be-a4bba02e11a4"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9318a6cc-c03f-4b9e-8631-4177dac6a94e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2b4e856f-3223-447c-8bdb-44f1d8600d24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6a959f7e-8106-4407-9e02-876a7ed75269"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar3"",
                    ""type"": ""PassThrough"",
                    ""id"": ""af595066-fc26-4dad-8730-87d90f6d61c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar4"",
                    ""type"": ""PassThrough"",
                    ""id"": ""975c2f05-bdbd-4399-81da-67bb637164e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar5"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bbc451e0-e176-4bd8-9a62-67182095ebda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar6"",
                    ""type"": ""PassThrough"",
                    ""id"": ""59f9c0fa-e49d-473c-80e6-5514b413b786"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar7"",
                    ""type"": ""PassThrough"",
                    ""id"": ""65de1fee-c309-4e0c-8105-d82386f5d2c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar8"",
                    ""type"": ""PassThrough"",
                    ""id"": ""255e8abf-a0c7-4159-bb86-f2143b0c5365"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HotBar9"",
                    ""type"": ""PassThrough"",
                    ""id"": ""57bb340a-2bae-4aaf-a4ef-2c61230a5fd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fc851367-8ee4-4f1a-a2fb-550ca30e1d60"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""dc173dea-4def-43a8-aa07-24135500d519"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6ebd5c09-87ab-4161-bdba-53874424ae7c"",
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
                    ""id"": ""48821e43-9571-4daf-9df2-eadd9d52de82"",
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
                    ""id"": ""92b4934a-76a5-481a-9ddb-6b995ed84ada"",
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
                    ""id"": ""d678685e-c588-48c4-be6f-f308a5174665"",
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
                    ""id"": ""8aabb6e2-6c9e-4576-8952-6136cb6b7909"",
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
                    ""id"": ""ede1cb80-0096-4e1a-a362-bf87355cb417"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d2316d1-fbc9-4103-9c27-cf17ea72c439"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e1c7776-0572-437a-8d83-e7bc6f917037"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e24c303-7f1e-41c8-8e89-3f53807739ba"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1881fc0-2368-4cc8-bcd2-dfada8886821"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f59dee9-a937-41e7-97a7-f2d069fcde0e"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db2dabfc-b2da-4998-93b2-cf553842199e"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53ef5d7a-733e-49f9-9575-2755f9b9967f"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e6f6c64-b4bb-4544-ad2a-771531d2c0e5"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bedf1f8c-0be5-456a-b23b-c15935e210a7"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b83ea95-0176-43c6-bee0-975dbb9f16b2"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HotBar3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_HotBar1 = m_Player.FindAction("HotBar1", throwIfNotFound: true);
        m_Player_HotBar2 = m_Player.FindAction("HotBar2", throwIfNotFound: true);
        m_Player_HotBar3 = m_Player.FindAction("HotBar3", throwIfNotFound: true);
        m_Player_HotBar4 = m_Player.FindAction("HotBar4", throwIfNotFound: true);
        m_Player_HotBar5 = m_Player.FindAction("HotBar5", throwIfNotFound: true);
        m_Player_HotBar6 = m_Player.FindAction("HotBar6", throwIfNotFound: true);
        m_Player_HotBar7 = m_Player.FindAction("HotBar7", throwIfNotFound: true);
        m_Player_HotBar8 = m_Player.FindAction("HotBar8", throwIfNotFound: true);
        m_Player_HotBar9 = m_Player.FindAction("HotBar9", throwIfNotFound: true);
        m_Player_MouseWheel = m_Player.FindAction("MouseWheel", throwIfNotFound: true);
        m_Player_UseItem = m_Player.FindAction("UseItem", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_HotBar1;
    private readonly InputAction m_Player_HotBar2;
    private readonly InputAction m_Player_HotBar3;
    private readonly InputAction m_Player_HotBar4;
    private readonly InputAction m_Player_HotBar5;
    private readonly InputAction m_Player_HotBar6;
    private readonly InputAction m_Player_HotBar7;
    private readonly InputAction m_Player_HotBar8;
    private readonly InputAction m_Player_HotBar9;
    private readonly InputAction m_Player_MouseWheel;
    private readonly InputAction m_Player_UseItem;
    public struct PlayerActions
    {
        private @PlayerController m_Wrapper;
        public PlayerActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @HotBar1 => m_Wrapper.m_Player_HotBar1;
        public InputAction @HotBar2 => m_Wrapper.m_Player_HotBar2;
        public InputAction @HotBar3 => m_Wrapper.m_Player_HotBar3;
        public InputAction @HotBar4 => m_Wrapper.m_Player_HotBar4;
        public InputAction @HotBar5 => m_Wrapper.m_Player_HotBar5;
        public InputAction @HotBar6 => m_Wrapper.m_Player_HotBar6;
        public InputAction @HotBar7 => m_Wrapper.m_Player_HotBar7;
        public InputAction @HotBar8 => m_Wrapper.m_Player_HotBar8;
        public InputAction @HotBar9 => m_Wrapper.m_Player_HotBar9;
        public InputAction @MouseWheel => m_Wrapper.m_Player_MouseWheel;
        public InputAction @UseItem => m_Wrapper.m_Player_UseItem;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @HotBar1.started += instance.OnHotBar1;
            @HotBar1.performed += instance.OnHotBar1;
            @HotBar1.canceled += instance.OnHotBar1;
            @HotBar2.started += instance.OnHotBar2;
            @HotBar2.performed += instance.OnHotBar2;
            @HotBar2.canceled += instance.OnHotBar2;
            @HotBar3.started += instance.OnHotBar3;
            @HotBar3.performed += instance.OnHotBar3;
            @HotBar3.canceled += instance.OnHotBar3;
            @HotBar4.started += instance.OnHotBar4;
            @HotBar4.performed += instance.OnHotBar4;
            @HotBar4.canceled += instance.OnHotBar4;
            @HotBar5.started += instance.OnHotBar5;
            @HotBar5.performed += instance.OnHotBar5;
            @HotBar5.canceled += instance.OnHotBar5;
            @HotBar6.started += instance.OnHotBar6;
            @HotBar6.performed += instance.OnHotBar6;
            @HotBar6.canceled += instance.OnHotBar6;
            @HotBar7.started += instance.OnHotBar7;
            @HotBar7.performed += instance.OnHotBar7;
            @HotBar7.canceled += instance.OnHotBar7;
            @HotBar8.started += instance.OnHotBar8;
            @HotBar8.performed += instance.OnHotBar8;
            @HotBar8.canceled += instance.OnHotBar8;
            @HotBar9.started += instance.OnHotBar9;
            @HotBar9.performed += instance.OnHotBar9;
            @HotBar9.canceled += instance.OnHotBar9;
            @MouseWheel.started += instance.OnMouseWheel;
            @MouseWheel.performed += instance.OnMouseWheel;
            @MouseWheel.canceled += instance.OnMouseWheel;
            @UseItem.started += instance.OnUseItem;
            @UseItem.performed += instance.OnUseItem;
            @UseItem.canceled += instance.OnUseItem;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @HotBar1.started -= instance.OnHotBar1;
            @HotBar1.performed -= instance.OnHotBar1;
            @HotBar1.canceled -= instance.OnHotBar1;
            @HotBar2.started -= instance.OnHotBar2;
            @HotBar2.performed -= instance.OnHotBar2;
            @HotBar2.canceled -= instance.OnHotBar2;
            @HotBar3.started -= instance.OnHotBar3;
            @HotBar3.performed -= instance.OnHotBar3;
            @HotBar3.canceled -= instance.OnHotBar3;
            @HotBar4.started -= instance.OnHotBar4;
            @HotBar4.performed -= instance.OnHotBar4;
            @HotBar4.canceled -= instance.OnHotBar4;
            @HotBar5.started -= instance.OnHotBar5;
            @HotBar5.performed -= instance.OnHotBar5;
            @HotBar5.canceled -= instance.OnHotBar5;
            @HotBar6.started -= instance.OnHotBar6;
            @HotBar6.performed -= instance.OnHotBar6;
            @HotBar6.canceled -= instance.OnHotBar6;
            @HotBar7.started -= instance.OnHotBar7;
            @HotBar7.performed -= instance.OnHotBar7;
            @HotBar7.canceled -= instance.OnHotBar7;
            @HotBar8.started -= instance.OnHotBar8;
            @HotBar8.performed -= instance.OnHotBar8;
            @HotBar8.canceled -= instance.OnHotBar8;
            @HotBar9.started -= instance.OnHotBar9;
            @HotBar9.performed -= instance.OnHotBar9;
            @HotBar9.canceled -= instance.OnHotBar9;
            @MouseWheel.started -= instance.OnMouseWheel;
            @MouseWheel.performed -= instance.OnMouseWheel;
            @MouseWheel.canceled -= instance.OnMouseWheel;
            @UseItem.started -= instance.OnUseItem;
            @UseItem.performed -= instance.OnUseItem;
            @UseItem.canceled -= instance.OnUseItem;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnHotBar1(InputAction.CallbackContext context);
        void OnHotBar2(InputAction.CallbackContext context);
        void OnHotBar3(InputAction.CallbackContext context);
        void OnHotBar4(InputAction.CallbackContext context);
        void OnHotBar5(InputAction.CallbackContext context);
        void OnHotBar6(InputAction.CallbackContext context);
        void OnHotBar7(InputAction.CallbackContext context);
        void OnHotBar8(InputAction.CallbackContext context);
        void OnHotBar9(InputAction.CallbackContext context);
        void OnMouseWheel(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
    }
}