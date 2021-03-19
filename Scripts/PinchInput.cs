namespace of2.TouchInput
{
    public struct PinchInput
    {
        /// <summary>
        /// ID of input that performed this pinch.
        /// </summary>
        public int InputId;
        
        /// <summary>
        /// Total distance travelled while pinching
        /// </summary>
        public float PinchDistance;

        /// <summary>
        /// Delta distance travelled while pinching
        /// </summary>
        public float PinchDeltaDistance;

        public override string ToString()
        {
            return $"InputId:{InputId}, PinchDistance:{PinchDistance}, PinchDeltaDistance:{PinchDeltaDistance}";
        }
    }
}