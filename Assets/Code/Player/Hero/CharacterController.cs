using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Player.Hero
{
    public class CharacterController : MonoBehaviour
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
        }



        private void OnDestroy()
        {
            _inputAction.Character.Movement.started -= SetMovemntDirection;
            _inputAction.Character.Movement.canceled -= SetMovemntDirection;

            _inputAction.Character.Click.canceled -= OnClick;
        }

        private void SetMovemntDirection(InputAction.CallbackContext obj)
        {
            _currentCharacter.SetDirection(obj.ReadValue<Vector2>());
        }
        private void OnClick(InputAction.CallbackContext obj)
        {
            _currentCharacter.Click();
        }
    }
}
