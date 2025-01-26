using CaptainNemo.Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace CaptainNemo.Controls.Logic
{
    /// <summary>
    /// Temperature control handler. This component is attached to the temperature valve
    /// </summary>
    public class PressureControl : ControlHandler
    {
        /// <summary>
        /// Control input attenuation
        /// </summary>
        [SerializeField] private float attenuation = 0.5f;

        /// <summary>
        /// Current pressure value.
        /// </summary>
        private float _pressure;
        
        [SerializeField] private float radiusThreshold = 10f;
        [SerializeField] private float angleThreshold = 15f;
        private Vector3 pivotPoint;
        private float lastRadius;
        private float lastAngle;
        private Vector3 lastValue;

        /// <summary>
        /// This component controls the pressure
        /// </summary>
        [SerializeField] private GlobalControlParam globalParam = GlobalControlParam.Pressure;

        /// <summary>
        /// Handle control: set pivot point
        /// </summary>
        protected override void OnHandle()
        {
            base.OnHandle();
            pivotPoint = UnityEngine.Input.mousePosition;
        }

        /// <summary>
        /// Processes control input by accumulating pressure.
        /// </summary>
        /// <param name="value">Input control vector.</param>
        protected override void OnControl(Vector2 value)
        {
            Vector3 mousePos = UnityEngine.Input.mousePosition;
            Vector3 lastOffset = (lastValue - pivotPoint).normalized;
            Vector3 currentOffset = (mousePos - pivotPoint).normalized;
            float angle = Vector3.SignedAngle(currentOffset, lastOffset, Vector3.forward);
            GameManager.AddPressure(angle * attenuation);
            lastValue = mousePos;
        }
        
        /// <summary>
        /// Get the actual pressure of the control
        /// </summary>
        /// <returns></returns>
        public override float GetControlValue()
        {
            return _pressure;
        }

        /// <summary>
        /// Controls the pressure
        /// </summary>
        /// <returns></returns>
        public override GlobalControlParam GetGlobalControlParam()
        {
            return globalParam;
        }
    }
}