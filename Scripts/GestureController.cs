#pragma warning disable 649

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace of2.TouchInput
{
    /// <summary>
    /// Controller that interprets takes pointer input from <see cref="PointerInputManager"/> and detects
    /// directional swipes, taps and pinches.
    /// </summary>
    public class GestureController : MonoBehaviour
    {
        [SerializeField]
        private PointerInputManager inputManager;

        // Maximum duration of a press before it can no longer be considered a tap.
        [SerializeField]
        private float maxTapDuration = 0.2f;

        // Maximum distance in screen units that a tap can drift from its original position before
        // it is no longer considered a tap.
        [SerializeField]
        private float maxTapDrift = 5.0f;

        // Maximum duration of a swipe before it is no longer considered to be a valid swipe.
        [SerializeField]
        private float maxSwipeDuration = 0.5f;

        // Minimum distance in screen units that a swipe must move before it is considered a swipe.
        // Note that if this is smaller or equal to maxTapDrift, then it is possible for a user action to be
        // returned as both a swipe and a tap.
        [SerializeField]
        private float minSwipeDistance = 10.0f;

        // How much a swipe should consistently be in the same direction before it is considered a swipe.
        [SerializeField]
        private float swipeDirectionSamenessThreshold = 0.6f;

        [Header("Debug"), SerializeField]
        private Text label;

        // Mapping of input IDs to their active gesture tracking objects.
        private readonly Dictionary<int, ActiveGesture> activeGestures = new Dictionary<int, ActiveGesture>();

        /// <summary>
        /// Event fired when the user presses first finger on the screen.
        /// </summary>
        public event Action<SwipeInput> PressedFirst;

        /// <summary>
        /// Event fired when the user presses second finger on the screen.
        /// </summary>
        public event Action<SwipeInput> PressedSecond;

        /// <summary>
        /// Event fired for every motion (possibly multiple times a frame) of a potential swipe gesture.
        /// </summary>
        public event Action<SwipeInput> PotentiallySwiped;

        /// <summary>
        /// Event fired when a user performs a swipe gesture.
        /// </summary>
        public event Action<SwipeInput> Swiped;

        /// <summary>
        /// Event fired when a user performs a tap gesture, on releasing.
        /// </summary>
        public event Action<TapInput> Tapped;
        
        /// <summary>
        /// Event fired during user performing pinching (every frame) 
        /// </summary>
        public event Action<PinchInput> Pinching;
        
        public event Action<PinchInput> PinchStarted;
        public event Action<PinchInput> PinchFinished;

        private bool _pinching;
        private bool _secondPressed;
        private float _pinchStartDistance;
        private float _pinchLastDistance;
        private Vector2 _pinchPosStartFirst;
        private Vector2 _pinchPosStartSecond;
        private Vector2 _posLastFirst;
        private Vector2 _posLastSecond;

        protected virtual void Awake()
        {
            inputManager.PressedFirst += OnPressedFirst;
            inputManager.DraggedFirst += OnDraggedFirst;
            inputManager.ReleasedFirst += OnReleasedFirst;
            
            inputManager.PressedSecond += OnPressedSecond;
            inputManager.DraggedSecond += OnDraggedSecond;
            inputManager.ReleasedSecond += OnReleasedSecond;
        }

        /// <summary>
        /// Checks whether a given active gesture will be a valid swipe.
        /// </summary>
        private bool IsValidSwipe(ref ActiveGesture gesture)
        {
            return gesture.TravelDistance >= minSwipeDistance &&
                (gesture.StartTime - gesture.EndTime) <= maxSwipeDuration &&
                gesture.SwipeDirectionSameness >= swipeDirectionSamenessThreshold;
        }

        /// <summary>
        /// Checks whether a given active gesture will be a valid tap.
        /// </summary>
        private bool IsValidTap(ref ActiveGesture gesture)
        {
            return gesture.TravelDistance <= maxTapDrift &&
                (gesture.StartTime - gesture.EndTime) <= maxTapDuration;
        }

        private void OnPressedFirst(PointerInput input, double time)
        {
            Debug.Assert(!activeGestures.ContainsKey(input.InputId));

            var newGesture = new ActiveGesture(input.InputId, input.Position, time);
            activeGestures.Add(input.InputId, newGesture);

            DebugInfo(newGesture);
            
            _posLastFirst = input.Position;

            if (_secondPressed)
            {
                _pinching = true;
                
                _pinchPosStartFirst = _posLastFirst;
                _pinchPosStartSecond = _posLastSecond;

                _pinchStartDistance = Vector2.Distance(_pinchPosStartFirst, _pinchPosStartSecond);
                _pinchLastDistance = _pinchStartDistance;
                
                PinchStarted?.Invoke(new PinchInput()
                {
                    InputId = input.InputId,
                    PinchDeltaDistance = 0,
                    PinchDistance = _pinchStartDistance,
                    Pointer0CurrentPosition = _pinchPosStartFirst,
                    Pointer0StartPosition = _pinchPosStartFirst,
                    Pointer1CurrentPosition = _pinchPosStartSecond,
                    Pointer1StartPosition = _pinchPosStartSecond
                });
            }

            PressedFirst?.Invoke(new SwipeInput(newGesture));
        }

        private void OnDraggedFirst(PointerInput input, double time)
        {
            if (!activeGestures.TryGetValue(input.InputId, out var existingGesture))
            {
                // Probably caught by UI, or the input was otherwise lost
                return;
            }

            existingGesture.SubmitPoint(input.Position, time);

            if (IsValidSwipe(ref existingGesture))
            {
                PotentiallySwiped?.Invoke(new SwipeInput(existingGesture));
            }

            if (_pinching)
            {
                _posLastFirst = input.Position;
                OnPinch(input);
            }
            
            DebugInfo(existingGesture);
        }

        private void OnReleasedFirst(PointerInput input, double time)
        {
            if (!activeGestures.TryGetValue(input.InputId, out var existingGesture))
            {
                // Probably caught by UI, or the input was otherwise lost
                return;
            }

            activeGestures.Remove(input.InputId);
            existingGesture.SubmitPoint(input.Position, time);

            if (IsValidSwipe(ref existingGesture))
            {
                Swiped?.Invoke(new SwipeInput(existingGesture));
            }

            if (IsValidTap(ref existingGesture))
            {
                Tapped?.Invoke(new TapInput(existingGesture));
            }

            if (_pinching)
            {
                _pinching = false;    
                
                var pinchDistance = Vector2.Distance(_posLastFirst, _posLastSecond);
                
                PinchFinished?.Invoke(new PinchInput()
                {
                    InputId = input.InputId,
                    PinchDistance = pinchDistance,
                    PinchDeltaDistance = pinchDistance - _pinchLastDistance,
                    Pointer0CurrentPosition = _posLastFirst,
                    Pointer0StartPosition = _pinchPosStartFirst,
                    Pointer1CurrentPosition = _posLastSecond,
                    Pointer1StartPosition = _pinchPosStartSecond
                });                
            }

            DebugInfo(existingGesture);
        }

        private void OnPressedSecond(PointerInput input, double time)
        {
            _pinching = true;
            _secondPressed = true;

            _pinchPosStartFirst = _posLastFirst;
            _pinchPosStartSecond = input.Position;
            _posLastSecond = _pinchPosStartSecond;

            _pinchStartDistance = Vector2.Distance(_pinchPosStartFirst, _pinchPosStartSecond);
            _pinchLastDistance = _pinchStartDistance;
            
            PinchStarted?.Invoke(new PinchInput()
            {
                InputId = input.InputId,
                PinchDeltaDistance = 0,
                PinchDistance = _pinchStartDistance,
                Pointer0CurrentPosition = _pinchPosStartFirst,
                Pointer0StartPosition = _pinchPosStartFirst,
                Pointer1CurrentPosition = _pinchPosStartSecond,
                Pointer1StartPosition = _pinchPosStartSecond
            });            
        }

        private void OnDraggedSecond(PointerInput input, double time)
        {
            _posLastSecond = input.Position;
            if (_pinching)
                OnPinch(input);
        }

        private void OnReleasedSecond(PointerInput input, double time)
        {
            _pinching = false;   
            _secondPressed = false;
            
            var pinchDistance = Vector2.Distance(_posLastFirst, _posLastSecond);
            
            PinchFinished?.Invoke(new PinchInput()
            {
                InputId = input.InputId,
                PinchDistance = pinchDistance,
                PinchDeltaDistance = pinchDistance - _pinchLastDistance,
                Pointer0CurrentPosition = _posLastFirst,
                Pointer0StartPosition = _pinchPosStartFirst,
                Pointer1CurrentPosition = _posLastSecond,
                Pointer1StartPosition = _pinchPosStartSecond
            });
        }

        private void OnPinch(PointerInput input)
        {
            var pinchDistance = Vector2.Distance(_posLastFirst, _posLastSecond);

            var pinchInput = new PinchInput()
            {
                InputId = input.InputId,
                PinchDistance = pinchDistance,
                PinchDeltaDistance = pinchDistance - _pinchLastDistance,
                Pointer0CurrentPosition = _posLastFirst,
                Pointer0StartPosition = _pinchPosStartFirst,
                Pointer1CurrentPosition = _posLastSecond,
                Pointer1StartPosition = _pinchPosStartSecond
            };
 
            //Debug.Log($"{nameof(GestureController)}: pinching: {pinchInput}");
            Pinching?.Invoke(pinchInput);
            
            _pinchLastDistance = pinchDistance;
        }

        private void DebugInfo(ActiveGesture gesture)
        {
            if (label == null) return;

            var builder = new StringBuilder();

            builder.AppendFormat("ID: {0}", gesture.InputId);
            builder.AppendLine();
            builder.AppendFormat("Start Position: {0}", gesture.StartPosition);
            builder.AppendLine();
            builder.AppendFormat("Position: {0}", gesture.EndPosition);
            builder.AppendLine();
            builder.AppendFormat("Duration: {0}", gesture.EndTime - gesture.StartTime);
            builder.AppendLine();
            builder.AppendFormat("Sameness: {0}", gesture.SwipeDirectionSameness);
            builder.AppendLine();
            builder.AppendFormat("Travel distance: {0}", gesture.TravelDistance);
            builder.AppendLine();
            builder.AppendFormat("Samples: {0}", gesture.Samples);
            builder.AppendLine();
            builder.AppendFormat("Realtime since startup: {0}", Time.realtimeSinceStartup);
            builder.AppendLine();
            builder.AppendFormat("Starting Timestamp: {0}", gesture.StartTime);
            builder.AppendLine();
            builder.AppendFormat("Ending Timestamp: {0}", gesture.EndTime);
            builder.AppendLine();

            label.text = builder.ToString();

            var worldStart = Camera.main.ScreenToWorldPoint(gesture.StartPosition);
            var worldEnd = Camera.main.ScreenToWorldPoint(gesture.EndPosition);

            worldStart.z += 5;
            worldEnd.z += 5;
        }
    }
}
