#pragma warning disable 649

using System;
using InputSamples.Controls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace of2.TouchInput
{
    /// <summary>
    /// Input manager that interprets action set up in PointerControl for point0 and point1 pointer actions
    /// point1 relevant only for touch.
    /// </summary>

    public class PointerInputManager : MonoBehaviour
    {
        /// <summary>
        /// Event fired when the user presses first finger on the screen.
        /// </summary>
        public event Action<PointerInput, double> PressedFirst;

        /// <summary>
        /// Event fired as the user drags along first finger on the screen.
        /// </summary>
        public event Action<PointerInput, double> DraggedFirst;

        /// <summary>
        /// Event fired when the user releases a first finger press.
        /// </summary>
        public event Action<PointerInput, double> ReleasedFirst;
        
        /// <summary>
        /// Event fired when the user presses first finger on the screen.
        /// </summary>
        public event Action<PointerInput, double> PressedSecond;

        /// <summary>
        /// Event fired as the user drags along first finger on the screen.
        /// </summary>
        public event Action<PointerInput, double> DraggedSecond;

        /// <summary>
        /// Event fired when the user releases a first finger press.
        /// </summary>
        public event Action<PointerInput, double> ReleasedSecond;        

        private bool m_DraggingFirst;
        private bool m_DraggingSecond;
        private PointerControls m_Controls;

        // These are useful for debugging, especially when touch simulation is on.
        [SerializeField] private bool m_UseMouse;
        [SerializeField] private bool m_UseTouch;

        protected virtual void Awake()
        {
            m_Controls = new PointerControls();

            m_Controls.pointer.point0.performed += OnActionFirst;
            // The action isn't likely to actually cancel as we've bound it to all kinds of inputs but we still
            // hook this up so in case the entire thing resets, we do get a call.
            m_Controls.pointer.point0.canceled += OnActionFirst;
            
            m_Controls.pointer.point1.performed += OnActionSecond;
            m_Controls.pointer.point1.canceled += OnActionSecond;

            SyncBindingMask();
        }

        protected virtual void OnEnable()
        {
            m_Controls?.Enable();
        }

        protected virtual void OnDisable()
        {
            m_Controls?.Disable();
        }

        protected void OnActionSecond(InputAction.CallbackContext context)
        {
            var drag = context.ReadValue<PointerInput>();

            if (drag.Contact && !m_DraggingSecond)
            {
                PressedSecond?.Invoke(drag, context.time);
                m_DraggingSecond = true;
                //Debug.Log($"{nameof(PointerInputManager)}: dragging started");
            }
            else if (drag.Contact && m_DraggingSecond)
            {
                DraggedSecond?.Invoke(drag, context.time);
                //Debug.Log($"{nameof(PointerInputManager)}: dragging continue");
            }
            else
            {
                ReleasedSecond?.Invoke(drag, context.time);
                m_DraggingSecond = false;
                //Debug.Log($"{nameof(PointerInputManager)}: dragging finished");
            }            
        }
        
        protected void OnActionFirst(InputAction.CallbackContext context)
        {
            var control = context.control;
            var device = control.device;
            
            var isMouseInput = device is Mouse;

            // Read our current pointer values.
            var drag = context.ReadValue<PointerInput>();
            if (isMouseInput)
                drag.InputId = Helpers.LeftMouseInputId;

            if (drag.Contact && !m_DraggingFirst)
            {
                PressedFirst?.Invoke(drag, context.time);
                m_DraggingFirst = true;
            }
            else if (drag.Contact && m_DraggingFirst)
            {
                DraggedFirst?.Invoke(drag, context.time);
            }
            else
            {
                ReleasedFirst?.Invoke(drag, context.time);
                m_DraggingFirst = false;
            }
        }

        private void SyncBindingMask()
        {
            if (m_Controls == null)
                return;

            if (m_UseMouse  && m_UseTouch)
            {
                m_Controls.bindingMask = null;
                return;
            }

            m_Controls.bindingMask = InputBinding.MaskByGroups(new[]
            {
                m_UseMouse ? "Mouse" : null,
                m_UseTouch ? "Touch" : null
            });
        }

        private void OnValidate()
        {
            SyncBindingMask();
        }
    }
}
