﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace CaptainNemo.Controls
{
    /// <summary>
    /// Vital parameters definition
    /// </summary>
    public enum GlobalControlParam : byte
    {
        /// <summary>
        /// Temperature global parameter
        /// </summary>
        Temperature,
        
        /// <summary>
        /// Pressure global parameter
        /// </summary>
        Pressure,
        
        /// <summary>
        /// Oxygen global parameter
        /// </summary>
        Oxygen
    }
    
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

        /// <summary>
        /// Get the value of the control
        /// </summary>
        /// <returns></returns>
        public float GetControlValue();

        public bool IsLocked();
        public void Lock();
        public void Unlock();
        public Transform LockTransform();
        
        /// <summary>
        /// Global parameter related by this controller
        /// </summary>
        /// <returns></returns>
        public GlobalControlParam GetGlobalControlParam();
    }
    
    /// <summary>
    /// Base implementation of a control handler that manages control state.
    /// </summary>
    public abstract class ControlHandler : MonoBehaviour, IControlHandler
    {
        [SerializeField] private Transform lockTransform;
        
        private bool _isBlockingControls;

        public bool IsLocked() => _isBlockingControls;

        public void Lock()
        {
            _isBlockingControls = true;
        }

        public void Unlock()
        {
            _isBlockingControls = false;
        }

        public Transform LockTransform() => lockTransform;
        
        /// <summary>
        /// Editor bindable event
        /// </summary>
        [SerializeField] 
        private UnityEvent<ControlHandler> onControlValueChanged; 
        
        [SerializeField] 
        private UnityEvent<ControlHandler> onHandle;
        
        [SerializeField] 
        private UnityEvent<ControlHandler> onRelease;
        
        /// <summary>
        /// Clamp the value of the control between
        /// </summary>
        [SerializeField]
        protected Vector2 clampValue = new Vector2(0, 100);
        
        /// <summary>
        /// Register control in controls manager
        /// </summary>
        private void Start()
        {
            ControlsManager.Instance.Register(this);
            OnStart();
        }

        protected virtual void OnStart() { }

        /// <summary>
        /// UnRegister control in controls manager
        /// </summary>
        private void OnDestroy()
        {
            ControlsManager.Instance.UnRegister(this);
        }

        /// <inheritdoc/>
        public virtual float GetControlValue()
        {
            return 0; 
        }

        /// <inheritdoc/>
        public abstract GlobalControlParam GetGlobalControlParam();

        /// <summary>
        /// Handles acquiring control, releasing previous handler if exists.
        /// </summary>
        public virtual void Handle()
        {
            if (_isBlockingControls)
            {
                Debug.Log($"[ControlHandler] Can't handle {this}, locked");
                return;
            }
            
            if (ControlsManager.ControlHandler != null)
            {
                ControlsManager.ControlHandler.Release();
            }
            
            ControlsManager.ControlHandler = this;
            Debug.Log($"Handle control: {this.name}");
            OnHandle();
            onHandle?.Invoke(this);
		}
        
        /// <summary>
        /// Virtual method called when control is acquired.
        /// Override to implement custom acquisition logic.
        /// </summary>
        protected virtual void OnHandle() { }


        /// <summary>
        /// Releases the current control handler.
        /// </summary>
        public void Release()
        {
            if (_isBlockingControls)
            {
                Debug.Log($"[ControlHandler] Can't release {this}, locked");
                return;
            }
            
            if (ControlsManager.ControlHandler != null)
            {
                ControlsManager.ControlHandler = null;
            }
            Debug.Log($"Release control: {this.name}");
            OnRelease();
            onRelease?.Invoke(this);
		}

        /// <summary>
        /// Virtual method called when control is released.
        /// Override to implement custom release logic.
        /// </summary>
        protected virtual void OnRelease() { }
        
        /// <summary>
        /// Processes control input.
        /// Override to implement custom control logic.
        /// </summary>
        /// <param name="value">Input control vector.</param>
        public void Control(Vector2 value)
        {
            if (_isBlockingControls) return;

            // Calculate new control value
            OnControl(value);
            onControlValueChanged?.Invoke(this);
        }

		/// <summary>
		/// Virtual method called when control value is updated.
		/// Override to implement custom release logic.
		/// </summary>
		/// <param name="value">Input to calculate new control value from</param>
		protected virtual void OnControl(Vector2 value) { }
    }

}