using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input {
    public interface IGameInput : PlayerAction.IPlayerActions { }

    public interface IGameInputEventSender
    {
        public event Action Interact;
        public event Action CancelInteract;
        public event Action<Vector2> Look;
    }

    [CreateAssetMenu(fileName = "InputReader", menuName = "Inputs")]
    public class InputReader : ScriptableObject, IGameInput, IGameInputEventSender
    {
        PlayerAction _inputActions;

        public void Enable()
        {
            if(_inputActions == null) {
                _inputActions = new PlayerAction();
                _inputActions.Player.SetCallbacks(this);
                Debug.Log("Created new player actions");
            }

            EnablePlayerCallbacks();
        }

        #region TOGGLE CALLBACKS
        private void EnablePlayerCallbacks()
        {
            _inputActions.Player.Enable();
            Debug.Log("Register player callbacks");
        }

        #endregion

        //**************************//
        //  GENERICS INPUT SECTION  //
        //**************************//

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log("Move");
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Interact?.Invoke();
            }

            if (context.canceled)
            {
                CancelInteract?.Invoke();
            }
        }

        public event Action Interact;
        public event Action CancelInteract;
        public event Action<Vector2> Look;
    }
}