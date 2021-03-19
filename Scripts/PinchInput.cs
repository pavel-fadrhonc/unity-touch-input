using UnityEngine;

namespace of2.TouchInput
{
    public struct PinchInput
    {
        /// <summary>
        /// ID of input that performed this pinch.
        /// </summary>
        public int InputId;
        
        /// <summary>
        /// Total distance bewtween pinch inputs
        /// </summary>
        public float PinchDistance;

        /// <summary>
        /// Delta distance travelled while pinching
        /// </summary>
        public float PinchDeltaDistance;

        public Vector2 Pointer0StartPosition;
        public Vector2 Pointer0CurrentPosition;
        
        public Vector2 Pointer1StartPosition;
        public Vector2 Pointer1CurrentPosition;

        public override string ToString()
        {
            return $"InputId:{InputId}, PinchDistance:{PinchDistance}, PinchDeltaDistance:{PinchDeltaDistance}";
        }
    }
}