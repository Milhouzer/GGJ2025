using UnityEngine;

namespace CaptainNemo.Controls
{
    /// <summary>
    /// Defines the contract for control handling in the game.
    /// </summary>
    public interface IControlHandler
    {
        /// <summary>
        /// Acquires control of the current handler.
        /// </summary>
        public void Handle();

        /// <summary>
        /// Releases the current control handler.
        /// </summary>
        public void Release();

        /// <summary>
        /// Processes input control values.
        /// </summary>
        /// <param name="value">Input control vector with x and y components.</param>
        public void Control(Vector2 value);

        public float GetControlValue();
    }
    
    /// <summary>
    /// Base implementation of a control handler that manages control state.
    /// </summary>
    public class ControlHandler : MonoBehaviour, IControlHandler
    {
        /// <summary>
        /// Register control in controls manager
        /// </summary>
        private void Start()
        {
            ControlsManager.Instance.Register(this);
        }

        /// <summary>
        /// UnRegister control in controls manager
        /// </summary>
        private void OnDestroy()
        {
            ControlsManager.Instance.UnRegister(this);
        }

        /// <summary>
        /// Virtual method called when control is acquired.
        /// Override to implement custom acquisition logic.
        /// </summary>
        protected virtual void OnHandle() { }
        
        /// <summary>
        /// Virtual method called when control is released.
        /// Override to implement custom release logic.
        /// </summary>
        protected virtual void OnRelease() { }

        /// <summary>
        /// Get the control of this value (i.e. temperature, pressure, starfish leg life state)
        /// </summary>
        /// <returns></returns>
        public virtual float GetControlValue()
        {
            return 0; 
        }
        
        /// <summary>
        /// Handles acquiring control, releasing previous handler if exists.
        /// </summary>
        public void Handle()
        {
            if (ControlsManager.ControlHandler != null)
            {
                ControlsManager.ControlHandler.Release();
            }
            
            ControlsManager.ControlHandler = this;
            Debug.Log($"Handle control: {this.name}");
            OnHandle();
        }

        /// <summary>
        /// Releases the current control handler.
        /// </summary>
        public void Release()
        {
            if (ControlsManager.ControlHandler != null)
            {
                ControlsManager.ControlHandler = null;
            }
            Debug.Log($"Release control: {this.name}");
            OnRelease();
        }

        /// <summary>
        /// Processes control input.
        /// Override to implement custom control logic.
        /// </summary>
        /// <param name="value">Input control vector.</param>
        public virtual void Control(Vector2 value) { }

    }

}