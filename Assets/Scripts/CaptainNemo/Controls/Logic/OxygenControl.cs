using UnityEngine;

namespace CaptainNemo.Controls.Logic
{
    /// <summary>
    /// Oxygen control handler. This component is attached to a bubble
    /// </summary>
    public class OxygenControl : ControlHandler
    {
        /// <summary>
        /// Current pressure value.
        /// </summary>
        private float _oxygen;

        /// <summary>
        /// This component controls the oxygen
        /// </summary>
        [SerializeField] private GlobalControlParam globalParam = GlobalControlParam.Oxygen;
        
        /// <summary>
        /// Custom logic when control is acquired.
        /// </summary>
        protected override void OnHandle()
        {
            // Custom acquisition logic specific to this control type
            
            // For example, the starfish leg may override this function to break and immediately release control after
            // as it shouldn't exist anymore.
            
            // Implement visual effects, sounds, etc.
        }

        /// <summary>
        /// Custom logic when control is released.
        /// </summary>
        protected override void OnRelease()
        {
            // Custom release logic specific to this control type
            
            // Implement on release business logic: visual effects, sounds, etc.
        }

        /// <summary>
        /// Processes control input by accumulating pressure.
        /// </summary>
        /// <param name="value">Input control vector.</param>
        protected override void OnControl(Vector2 value)
        {
            _oxygen = Mathf.Clamp(_oxygen + value.y, clampValue.x, clampValue.y);
        }
        
        /// <summary>
        /// Get the actual pressure of the control
        /// </summary>
        /// <returns></returns>
        public override float GetControlValue()
        {
            return _oxygen;
        }
        
        /// <summary>
        /// Controls the oxygen level
        /// </summary>
        /// <returns></returns>
        public override GlobalControlParam GetGlobalControlParam()
        {
            return globalParam;
        }
    }
}