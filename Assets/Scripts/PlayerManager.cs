using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MovementController))]
public class PlayerManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private MovementController _movementController;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementController = GetComponent<MovementController>();
        _playerInput.InputEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_movementController.IsMoving) return;
        _playerInput.GetKeyInput();
        if (Mathf.Approximately(_playerInput.Vertical, 0))
        {
            if (_playerInput.Horizontal > 0)
            {
                _movementController.MoveLeft();
            }

            if (_playerInput.Horizontal < 0)
            {
                _movementController.MoveRight();
            }
        }

        if (Mathf.Approximately(_playerInput.Horizontal, 0))
        {
            if (_playerInput.Vertical > 0)
            {
                _movementController.MoveBack();
            }

            if (_playerInput.Vertical < 0)
            {
                _movementController.MoveForward();
            }
        }
    }
}
