//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/PlayerActions.inputactions
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

public partial class @PlayerActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""28c4ae1b-af13-47e5-b9dc-7eabd6548946"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""785657b8-6f38-4702-8f71-a6ab8f38bcfd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""59e267b1-b80d-4bea-9e0f-fa2feca923cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""dee79333-1a8d-4c32-a6e6-7745dcb2a933"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShootDirection"",
                    ""type"": ""Value"",
                    ""id"": ""8bee7192-8eae-44f3-9489-5a059ba89901"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""f227c6ed-ab67-4e17-ae46-2f655ffb53ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Previous Item"",
                    ""type"": ""Button"",
                    ""id"": ""79ad8d02-5178-4b94-9d98-0c6d0a8ca5a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Next Item"",
                    ""type"": ""Button"",
                    ""id"": ""36d9e607-9313-472c-8a0f-754e018cfb35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ef1b4e2a-8afb-407b-ba0e-04fb3ef65af9"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e35c7aab-ede1-4d41-87d9-0bb2bda0eb4a"",
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
                    ""id"": ""5fd49e9c-3902-4bdb-a366-4836627c9c36"",
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
                    ""id"": ""af88fc18-1830-4f23-864b-9bc5ce89e583"",
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
                    ""id"": ""e5599be8-3683-4cc9-8b3e-af7a3b2a0617"",
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
                    ""id"": ""ba99f831-c3ac-4b2d-90e4-33db753b5fae"",
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
                    ""id"": ""9dc1b4e4-fc9f-4e51-9b60-6d01d3c0dd70"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8e9cfff-f7d1-488f-9483-c0de7ba20cc6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea39ae92-8f52-415a-9ea5-5b90a7dc3839"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98a7ecdf-45e2-4346-9c46-afa9a56a6934"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""2642651f-30ef-4559-8136-75c8a1795ba0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootDirection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""953bbacf-9f37-4a5c-95b0-de76e460af72"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""88f6e3f1-ac9c-4c72-81e1-608ab85a6e58"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""37d7b3fa-d6d2-4a1c-b6ad-b650ac853e38"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""832c5085-e87b-4e6d-b5c7-98d769563f22"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6cb22ae7-65c1-44c5-ac7d-28c3e7f0a8f0"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c49d73b-a413-4065-8974-9f0077a15355"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b77e6643-d659-4447-92d8-534753f7bf9c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64a8948d-52e5-4047-86d4-28528eb99095"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Previous Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b37f562e-3eca-4f6c-857d-fa2b5c4ade87"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Previous Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79199a61-6f6f-4aad-9d38-6e083e49580e"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CharacterSelector"",
            ""id"": ""51429c8a-26e2-4999-be3b-62785d964e33"",
            ""actions"": [
                {
                    ""name"": ""SelectButtonRight"",
                    ""type"": ""Button"",
                    ""id"": ""0f334947-6e12-4b95-92e4-84e6ba4e4726"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectButtonLeft"",
                    ""type"": ""Button"",
                    ""id"": ""05dc7f9f-19a6-4279-9cf8-00249c9af09b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectButtonDown"",
                    ""type"": ""Button"",
                    ""id"": ""45554166-8c9d-4fe3-8fea-c92ddbe346bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectButtonUp"",
                    ""type"": ""Button"",
                    ""id"": ""956c4fe8-5092-4600-b0b3-0362f3f4f5d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ClickButton"",
                    ""type"": ""Button"",
                    ""id"": ""396c2f8f-b8f6-4560-9d3d-665a5cd62007"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""862ccd7a-0511-42c4-83f4-49988033f80c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""SelectButtonRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3030d9c9-9f1e-4918-b191-0618805965bf"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectButtonRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8dec963-8cbe-4c7c-93e5-adc9c6db6c9a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b9e76bf-dd19-441c-94ef-7088b2e305c8"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""SelectButtonLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be650a69-49ec-4479-bcb7-bd7923b1c473"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectButtonLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dcbd155-dbe8-4d4e-b239-bdf5efecdf96"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""SelectButtonDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f222059-2c84-4fae-97e9-15f3c3740d11"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectButtonDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c0d40d3-e62c-4bd9-b397-ffa19c574f08"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""SelectButtonUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7c99048-368d-4a22-b5f0-4f5b7d236d56"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectButtonUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Movement = m_Player1.FindAction("Movement", throwIfNotFound: true);
        m_Player1_Grab = m_Player1.FindAction("Grab", throwIfNotFound: true);
        m_Player1_Shoot = m_Player1.FindAction("Shoot", throwIfNotFound: true);
        m_Player1_ShootDirection = m_Player1.FindAction("ShootDirection", throwIfNotFound: true);
        m_Player1_UseItem = m_Player1.FindAction("UseItem", throwIfNotFound: true);
        m_Player1_PreviousItem = m_Player1.FindAction("Previous Item", throwIfNotFound: true);
        m_Player1_NextItem = m_Player1.FindAction("Next Item", throwIfNotFound: true);
        // CharacterSelector
        m_CharacterSelector = asset.FindActionMap("CharacterSelector", throwIfNotFound: true);
        m_CharacterSelector_SelectButtonRight = m_CharacterSelector.FindAction("SelectButtonRight", throwIfNotFound: true);
        m_CharacterSelector_SelectButtonLeft = m_CharacterSelector.FindAction("SelectButtonLeft", throwIfNotFound: true);
        m_CharacterSelector_SelectButtonDown = m_CharacterSelector.FindAction("SelectButtonDown", throwIfNotFound: true);
        m_CharacterSelector_SelectButtonUp = m_CharacterSelector.FindAction("SelectButtonUp", throwIfNotFound: true);
        m_CharacterSelector_ClickButton = m_CharacterSelector.FindAction("ClickButton", throwIfNotFound: true);
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

    // Player1
    private readonly InputActionMap m_Player1;
    private List<IPlayer1Actions> m_Player1ActionsCallbackInterfaces = new List<IPlayer1Actions>();
    private readonly InputAction m_Player1_Movement;
    private readonly InputAction m_Player1_Grab;
    private readonly InputAction m_Player1_Shoot;
    private readonly InputAction m_Player1_ShootDirection;
    private readonly InputAction m_Player1_UseItem;
    private readonly InputAction m_Player1_PreviousItem;
    private readonly InputAction m_Player1_NextItem;
    public struct Player1Actions
    {
        private @PlayerActions m_Wrapper;
        public Player1Actions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player1_Movement;
        public InputAction @Grab => m_Wrapper.m_Player1_Grab;
        public InputAction @Shoot => m_Wrapper.m_Player1_Shoot;
        public InputAction @ShootDirection => m_Wrapper.m_Player1_ShootDirection;
        public InputAction @UseItem => m_Wrapper.m_Player1_UseItem;
        public InputAction @PreviousItem => m_Wrapper.m_Player1_PreviousItem;
        public InputAction @NextItem => m_Wrapper.m_Player1_NextItem;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void AddCallbacks(IPlayer1Actions instance)
        {
            if (instance == null || m_Wrapper.m_Player1ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player1ActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Grab.started += instance.OnGrab;
            @Grab.performed += instance.OnGrab;
            @Grab.canceled += instance.OnGrab;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @ShootDirection.started += instance.OnShootDirection;
            @ShootDirection.performed += instance.OnShootDirection;
            @ShootDirection.canceled += instance.OnShootDirection;
            @UseItem.started += instance.OnUseItem;
            @UseItem.performed += instance.OnUseItem;
            @UseItem.canceled += instance.OnUseItem;
            @PreviousItem.started += instance.OnPreviousItem;
            @PreviousItem.performed += instance.OnPreviousItem;
            @PreviousItem.canceled += instance.OnPreviousItem;
            @NextItem.started += instance.OnNextItem;
            @NextItem.performed += instance.OnNextItem;
            @NextItem.canceled += instance.OnNextItem;
        }

        private void UnregisterCallbacks(IPlayer1Actions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Grab.started -= instance.OnGrab;
            @Grab.performed -= instance.OnGrab;
            @Grab.canceled -= instance.OnGrab;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @ShootDirection.started -= instance.OnShootDirection;
            @ShootDirection.performed -= instance.OnShootDirection;
            @ShootDirection.canceled -= instance.OnShootDirection;
            @UseItem.started -= instance.OnUseItem;
            @UseItem.performed -= instance.OnUseItem;
            @UseItem.canceled -= instance.OnUseItem;
            @PreviousItem.started -= instance.OnPreviousItem;
            @PreviousItem.performed -= instance.OnPreviousItem;
            @PreviousItem.canceled -= instance.OnPreviousItem;
            @NextItem.started -= instance.OnNextItem;
            @NextItem.performed -= instance.OnNextItem;
            @NextItem.canceled -= instance.OnNextItem;
        }

        public void RemoveCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer1Actions instance)
        {
            foreach (var item in m_Wrapper.m_Player1ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player1ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // CharacterSelector
    private readonly InputActionMap m_CharacterSelector;
    private List<ICharacterSelectorActions> m_CharacterSelectorActionsCallbackInterfaces = new List<ICharacterSelectorActions>();
    private readonly InputAction m_CharacterSelector_SelectButtonRight;
    private readonly InputAction m_CharacterSelector_SelectButtonLeft;
    private readonly InputAction m_CharacterSelector_SelectButtonDown;
    private readonly InputAction m_CharacterSelector_SelectButtonUp;
    private readonly InputAction m_CharacterSelector_ClickButton;
    public struct CharacterSelectorActions
    {
        private @PlayerActions m_Wrapper;
        public CharacterSelectorActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectButtonRight => m_Wrapper.m_CharacterSelector_SelectButtonRight;
        public InputAction @SelectButtonLeft => m_Wrapper.m_CharacterSelector_SelectButtonLeft;
        public InputAction @SelectButtonDown => m_Wrapper.m_CharacterSelector_SelectButtonDown;
        public InputAction @SelectButtonUp => m_Wrapper.m_CharacterSelector_SelectButtonUp;
        public InputAction @ClickButton => m_Wrapper.m_CharacterSelector_ClickButton;
        public InputActionMap Get() { return m_Wrapper.m_CharacterSelector; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterSelectorActions set) { return set.Get(); }
        public void AddCallbacks(ICharacterSelectorActions instance)
        {
            if (instance == null || m_Wrapper.m_CharacterSelectorActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharacterSelectorActionsCallbackInterfaces.Add(instance);
            @SelectButtonRight.started += instance.OnSelectButtonRight;
            @SelectButtonRight.performed += instance.OnSelectButtonRight;
            @SelectButtonRight.canceled += instance.OnSelectButtonRight;
            @SelectButtonLeft.started += instance.OnSelectButtonLeft;
            @SelectButtonLeft.performed += instance.OnSelectButtonLeft;
            @SelectButtonLeft.canceled += instance.OnSelectButtonLeft;
            @SelectButtonDown.started += instance.OnSelectButtonDown;
            @SelectButtonDown.performed += instance.OnSelectButtonDown;
            @SelectButtonDown.canceled += instance.OnSelectButtonDown;
            @SelectButtonUp.started += instance.OnSelectButtonUp;
            @SelectButtonUp.performed += instance.OnSelectButtonUp;
            @SelectButtonUp.canceled += instance.OnSelectButtonUp;
            @ClickButton.started += instance.OnClickButton;
            @ClickButton.performed += instance.OnClickButton;
            @ClickButton.canceled += instance.OnClickButton;
        }

        private void UnregisterCallbacks(ICharacterSelectorActions instance)
        {
            @SelectButtonRight.started -= instance.OnSelectButtonRight;
            @SelectButtonRight.performed -= instance.OnSelectButtonRight;
            @SelectButtonRight.canceled -= instance.OnSelectButtonRight;
            @SelectButtonLeft.started -= instance.OnSelectButtonLeft;
            @SelectButtonLeft.performed -= instance.OnSelectButtonLeft;
            @SelectButtonLeft.canceled -= instance.OnSelectButtonLeft;
            @SelectButtonDown.started -= instance.OnSelectButtonDown;
            @SelectButtonDown.performed -= instance.OnSelectButtonDown;
            @SelectButtonDown.canceled -= instance.OnSelectButtonDown;
            @SelectButtonUp.started -= instance.OnSelectButtonUp;
            @SelectButtonUp.performed -= instance.OnSelectButtonUp;
            @SelectButtonUp.canceled -= instance.OnSelectButtonUp;
            @ClickButton.started -= instance.OnClickButton;
            @ClickButton.performed -= instance.OnClickButton;
            @ClickButton.canceled -= instance.OnClickButton;
        }

        public void RemoveCallbacks(ICharacterSelectorActions instance)
        {
            if (m_Wrapper.m_CharacterSelectorActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharacterSelectorActions instance)
        {
            foreach (var item in m_Wrapper.m_CharacterSelectorActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharacterSelectorActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharacterSelectorActions @CharacterSelector => new CharacterSelectorActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayer1Actions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnShootDirection(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
        void OnPreviousItem(InputAction.CallbackContext context);
        void OnNextItem(InputAction.CallbackContext context);
    }
    public interface ICharacterSelectorActions
    {
        void OnSelectButtonRight(InputAction.CallbackContext context);
        void OnSelectButtonLeft(InputAction.CallbackContext context);
        void OnSelectButtonDown(InputAction.CallbackContext context);
        void OnSelectButtonUp(InputAction.CallbackContext context);
        void OnClickButton(InputAction.CallbackContext context);
    }
}
