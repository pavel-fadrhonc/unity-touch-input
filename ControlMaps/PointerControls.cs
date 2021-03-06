// GENERATED AUTOMATICALLY FROM 'Packages/com.oakus.frameworks.unity-touch-input/ControlMaps/PointerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace InputSamples.Controls
{
    public class @PointerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PointerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PointerControls"",
    ""maps"": [
        {
            ""name"": ""pointer"",
            ""id"": ""03c7e318-7062-4e8e-8039-96e82fd74b50"",
            ""actions"": [
                {
                    ""name"": ""point0"",
                    ""type"": ""Value"",
                    ""id"": ""eeed27bd-2609-465e-b6ec-44fb11edf534"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""point1"",
                    ""type"": ""Value"",
                    ""id"": ""d40f6bff-3606-461f-a327-c665f33e526d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Mouse"",
                    ""id"": ""3165094d-d5ce-49ee-8bff-850d584cc105"",
                    ""path"": ""PointerInput"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point0"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Contact"",
                    ""id"": ""18060b67-edf3-404f-a0c0-627ac3a62cc2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Position"",
                    ""id"": ""f32a058a-a83e-48d0-a995-e8abbb720606"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Touch0"",
                    ""id"": ""83e3c436-8372-44e2-9090-ab2557b49e88"",
                    ""path"": ""PointerInput"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point0"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""contact"",
                    ""id"": ""3c399beb-67a9-47c5-aa45-08ed64ab606e"",
                    ""path"": ""<Touchscreen>/touch0/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""point0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""position"",
                    ""id"": ""fcd0e12f-cce4-4e1f-b3bc-5a140d600e36"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""point0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""radius"",
                    ""id"": ""c2d823e9-c363-472e-ad86-5e547898161c"",
                    ""path"": ""<Touchscreen>/touch0/radius"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""point0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""pressure"",
                    ""id"": ""ef392420-c0ba-4be8-a052-be82979ac6e5"",
                    ""path"": ""<Touchscreen>/touch0/pressure"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""point0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""inputId"",
                    ""id"": ""9d1470a4-4216-48d7-9d7d-d277b38db693"",
                    ""path"": ""<Touchscreen>/touch0/touchId"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""point0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Touch1"",
                    ""id"": ""f11ea22a-8ada-4024-bcd1-45e263f95556"",
                    ""path"": ""PointerInput"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""contact"",
                    ""id"": ""5603bdbf-055a-4d56-bd69-c9241ecb1bb7"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""position"",
                    ""id"": ""cf50c940-b296-4bc3-b6a8-4f0e53342950"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""radius"",
                    ""id"": ""f6560153-5f29-477a-bc67-179efc5a02d8"",
                    ""path"": ""<Touchscreen>/touch1/radius"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""pressure"",
                    ""id"": ""fd33ee30-661c-4e6a-ad6e-2e8bf9df25e8"",
                    ""path"": ""<Touchscreen>/touch1/pressure"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""inputId"",
                    ""id"": ""5c520ecb-cf4f-4458-b444-8b5255cd8f4e"",
                    ""path"": ""<Touchscreen>/touch1/touchId"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""point1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": []
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": []
        }
    ]
}");
            // pointer
            m_pointer = asset.FindActionMap("pointer", throwIfNotFound: true);
            m_pointer_point0 = m_pointer.FindAction("point0", throwIfNotFound: true);
            m_pointer_point1 = m_pointer.FindAction("point1", throwIfNotFound: true);
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

        // pointer
        private readonly InputActionMap m_pointer;
        private IPointerActions m_PointerActionsCallbackInterface;
        private readonly InputAction m_pointer_point0;
        private readonly InputAction m_pointer_point1;
        public struct PointerActions
        {
            private @PointerControls m_Wrapper;
            public PointerActions(@PointerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @point0 => m_Wrapper.m_pointer_point0;
            public InputAction @point1 => m_Wrapper.m_pointer_point1;
            public InputActionMap Get() { return m_Wrapper.m_pointer; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PointerActions set) { return set.Get(); }
            public void SetCallbacks(IPointerActions instance)
            {
                if (m_Wrapper.m_PointerActionsCallbackInterface != null)
                {
                    @point0.started -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint0;
                    @point0.performed -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint0;
                    @point0.canceled -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint0;
                    @point1.started -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint1;
                    @point1.performed -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint1;
                    @point1.canceled -= m_Wrapper.m_PointerActionsCallbackInterface.OnPoint1;
                }
                m_Wrapper.m_PointerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @point0.started += instance.OnPoint0;
                    @point0.performed += instance.OnPoint0;
                    @point0.canceled += instance.OnPoint0;
                    @point1.started += instance.OnPoint1;
                    @point1.performed += instance.OnPoint1;
                    @point1.canceled += instance.OnPoint1;
                }
            }
        }
        public PointerActions @pointer => new PointerActions(this);
        private int m_MouseSchemeIndex = -1;
        public InputControlScheme MouseScheme
        {
            get
            {
                if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
                return asset.controlSchemes[m_MouseSchemeIndex];
            }
        }
        private int m_TouchSchemeIndex = -1;
        public InputControlScheme TouchScheme
        {
            get
            {
                if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
                return asset.controlSchemes[m_TouchSchemeIndex];
            }
        }
        public interface IPointerActions
        {
            void OnPoint0(InputAction.CallbackContext context);
            void OnPoint1(InputAction.CallbackContext context);
        }
    }
}
