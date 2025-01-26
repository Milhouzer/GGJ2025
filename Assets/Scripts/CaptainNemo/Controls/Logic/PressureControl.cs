﻿using CaptainNemo.Game;
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
        private Vector2 pivotPoint;
        private float lastRadius;
        private float lastAngle;

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
            // Calculate vector from pivot to current mouse position
            Vector2 centerOffset = value - pivotPoint;
    
            // Calculate current radius and angle
            float currentRadius = centerOffset.magnitude;
            float currentAngle = Mathf.Atan2(centerOffset.y, centerOffset.x);
    
            // Compare with previous measurements to detect circular motion
            float radiusDelta = Mathf.Abs(currentRadius - lastRadius);
            float angleDelta = Mathf.Abs(Mathf.DeltaAngle(lastAngle, currentAngle));
    
            // Threshold values can be adjusted based on precision needed
            bool isCircularMotion = 
                radiusDelta < radiusThreshold && 
                angleDelta < angleThreshold;
    
            if (isCircularMotion)
            {
                GameManager.AddPressure(angleDelta * attenuation);
                _pressure = angleDelta * attenuation;
            }
    
            // Update last known state
            lastRadius = currentRadius;
            lastAngle = currentAngle;
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