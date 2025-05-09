using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class TouchInputController : MonoBehaviour
    {
        private Controls _controls;

        public event Action OnClickPerformed;
        public event Action OnClickCancel;
        public event Action<Vector2> OnJoystickPerformed;

        public void Init()
        {
            _controls = new Controls();
            _controls.Enable();
            Subscribe();
        }

        private void Subscribe()
        {
            _controls.UI.Click.performed += OnClickPerformedHandler;
            _controls.UI.Click.canceled += OnClickCancelHandler;
            _controls.Player.Move.performed += OnJoystickPerformedHandler;
            _controls.Player.Move.canceled += OnJoystickPerformedHandler;
        }

        private void Unsubscribe()
        {
            _controls.UI.Click.performed -= OnClickPerformedHandler; 
            _controls.UI.Click.canceled -= OnClickCancelHandler;
            _controls.Player.Move.performed -= OnJoystickPerformedHandler;
            _controls.Player.Move.canceled -= OnJoystickPerformedHandler;
        }

        private void OnClickCancelHandler(InputAction.CallbackContext obj)
        {
            OnClickCancel?.Invoke();
        }

        private void OnClickPerformedHandler(InputAction.CallbackContext obj)
        {
            OnClickPerformed?.Invoke();
        }

        private void OnJoystickPerformedHandler(InputAction.CallbackContext obj)
        {
            //Debug.Log($"Input: {obj.ReadValue<Vector2>()}");
            OnJoystickPerformed?.Invoke(obj.ReadValue<Vector2>());
        }
        
        public void Free()
        {
            Unsubscribe();
        }
    }
}
