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

            _inputAction.Character.Jump.started += OnJumpStart;
            _inputAction.Character.Jump.canceled += OnJumpCancel;

            _inputAction.Character.Interactable.canceled += Interactive;
        }



        private void OnDestroy()
        {
            _inputAction.Character.Movement.started -= SetMovemntDirection;
            _inputAction.Character.Movement.canceled -= SetMovemntDirection;

            _inputAction.Character.Click.canceled -= OnClick;

            _inputAction.Character.Jump.started -= OnJumpStart;
            _inputAction.Character.Jump.canceled -= OnJumpCancel;

            _inputAction.Character.Interactable.canceled -= Interactive;
        }

        private void SetMovemntDirection(InputAction.CallbackContext obj)
        {
            _currentCharacter.CurrentInputDirection = (obj.ReadValue<float>());
        }

        private void OnJumpStart(InputAction.CallbackContext obj)
        {
            _currentCharacter.CountJumps ++;
            _currentCharacter.SetAnimationAirJump();
            _currentCharacter.IsJump = true;
        }
        private void OnJumpCancel(InputAction.CallbackContext obj)
        {
            _currentCharacter.JumpTimeJumps = 0f;
            _currentCharacter.IsJump = false;
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            //_currentCharacter.Click();
        }

        private void Interactive(InputAction.CallbackContext obj)
        {
            _currentCharacter.Interactions();
        }
    }
}
