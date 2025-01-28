using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Player.Hero
{
    public class CharacterInputHandler : MonoBehaviour
    {
        private InputPlayerAction _inputAction;
        [SerializeField]
        private Character _currentCharacter;

        private void Awake()
        {
            _inputAction = new InputPlayerAction();

            _currentCharacter = GetComponent<Character>();

            _inputAction.Character.Enable();

            _inputAction.Character.Movement.performed += SetMovemntDirection;
            _inputAction.Character.Movement.canceled += SetMovemntDirection;

            _inputAction.Character.Click.canceled += OnClick;

            _inputAction.Character.Jump.started += OnJump;
            _inputAction.Character.Jump.canceled += OnJump;
        }



        private void OnDestroy()
        {
            _inputAction.Character.Movement.started -= SetMovemntDirection;
            _inputAction.Character.Movement.canceled -= SetMovemntDirection;

            _inputAction.Character.Click.canceled -= OnClick;

            _inputAction.Character.Jump.started -= OnJump;
            _inputAction.Character.Jump.canceled -= OnJump;
        }

        private void SetMovemntDirection(InputAction.CallbackContext obj)
        {
            _currentCharacter.CurrentInputDirection = (obj.ReadValue<float>());
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            _currentCharacter.IsJump = (Convert.ToBoolean(obj.ReadValue<float>()));
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            //_currentCharacter.Click();
        }
    }
}
