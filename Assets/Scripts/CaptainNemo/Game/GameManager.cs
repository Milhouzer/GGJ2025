﻿using CaptainNemo.Controls;
using CaptainNemo.Controls.Logic;
using Input;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using CaptainNemo.Player;

namespace CaptainNemo.Game
{
    /// <summary>
    /// Manages core game logic, input handling, and game state.
    /// Implements singleton pattern for global access.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        /// <summary>
        /// Input reader for game interactions and controls.
        /// </summary>
        [FormerlySerializedAs("input")] 
        [SerializeField] private InputReader inputReader;

        /// <summary>
        /// Provides access to the current input reader.
        /// </summary>
        public InputReader InputReader => inputReader;
        
        /// <summary>
        /// Reference to the player's main camera.
        /// </summary>
        [SerializeField] private Camera playerCamera;

        /// <summary>
        /// Current game object under mouse cursor/camera focus.
        /// </summary>
        public static GameObject TargetObject { get; private set; }

        /// <summary>
        /// Player's diver instance for game interactions.
        /// </summary>
        [SerializeField] private Diver _diver;
        
        /// <summary>
        /// Initializes game state, input handling, and diver.
        /// Sets up input event subscriptions.
        /// </summary>
        private void Start()
        {
            if (inputReader == null)
            {
                Debug.LogError("Can not start game without input reader");
                return;
            }

            inputReader = Instantiate(inputReader);
            inputReader.Interact += Interact;
            inputReader.CancelInteract += CancelInteract;
            inputReader.Look += Look;
            inputReader.Move += Move;
            inputReader.Enable();
        }
        
        /// <summary>
        /// Updates target object based on mouse cursor raycast.
        /// Tracks game object under camera/mouse focus.
        /// </summary>
        private void Update()
        {
            Move(inputReader.MoveInput);
            if(!playerCamera) return;
    
            Ray ray = playerCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            { 
                Debug.DrawRay(ray.origin, Vector3.forward * hit.distance, Color.yellow); 
                GameManager.TargetObject = hit.transform.gameObject;
                // Debug.Log($"New target: {GameManager.TargetObject.name}");
            }
            else
            {
                if (GameManager.TargetObject)
                {
                    GameManager.TargetObject = null;
                    // Debug.Log("New null target");
                }
            }
        }

        public static void AddOxygen(float value)
        {
            GameManager.Instance._diver.Oxygen(value);
        }
        
        public static void AddTemperature(float value)
        {
            GameManager.Instance._diver.Temperature(value);
        }

        public static void AddPressure(float value)
        {
            GameManager.Instance._diver.Pressure(value);
        }

        public static PressureControl GetPressureControl()
        {
            return GameManager.Instance._diver.PressureControl;
        }

        public static TemperatureControl GetTemperatureControl()
        {
            return GameManager.Instance._diver.TemperatureControl;
        }

        public static float GetMaxOxygenValue()
        {
            return GameManager.Instance._diver.OxygenParam.Range.y;
        }

        public static float GetMinOxygenValue()
        {
            return GameManager.Instance._diver.OxygenParam.Range.x;
        }

        public static float GetMaxTemperatureValue()
        {
            return GameManager.Instance._diver.TemperatureParam.Range.y;
        }

        public static float GetMinTemperatureValue()
        {
            return GameManager.Instance._diver.TemperatureParam.Range.x;
        }
        
        public static float GetMaxPressureValue()
        {
            return GameManager.Instance._diver.PressureParam.Range.y;
        }
        
        public static float GetMinPressureValue()
        {
            return GameManager.Instance._diver.PressureParam.Range.x;
        }

        /// <summary>
        /// Handles object interaction when interaction input is triggered.
        /// Attempts to call Handle() on IControlHandler component of target object.
        /// </summary>
        private void Interact()
        {
            if (TargetObject == null) return;
            
            IControlHandler handler = TargetObject.GetComponent<IControlHandler>();
            if (handler == null || handler.GetGlobalControlParam() == GlobalControlParam.Oxygen) return;
            
            handler.Handle();
        }

        /// <summary>
        /// Processes look input by passing control vector to current control handler.
        /// </summary>
        /// <param name="value">2D input vector representing look direction/intensity</param>
        private void Look(Vector2 value)
        {
            if (ControlsManager.ControlHandler == null || ControlsManager.ControlHandler.GetGlobalControlParam() == GlobalControlParam.Oxygen) return;
            // Debug.Log($"OnLook {value}");
            ControlsManager.ControlHandler?.Control(value);
        }

        /// <summary>
        /// Processes move input by passing control vector to current control handler.
        /// </summary>
        /// <param name="value">2D input vector representing move direction/intensity</param>
        private void Move(Vector2 value)
        {
            _diver.Mouth?.Move(value);
        }
        
        /// <summary>
        /// Handles interaction cancellation by releasing current control handler.
        /// </summary>
        private void CancelInteract()
        {
            ControlsManager.ControlHandler?.Release();
        }
    }
}