using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MovementController))]
public class PlayerManager : MonoBehaviour
{
    private MovementController _movementController;

    public PlayerInput PlayerInput { get; private set; }

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        _movementController = GetComponent<MovementController>();
        PlayerInput.InputEnabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_movementController.IsMoving) return;
        PlayerInput.GetKeyInput();
        if (Mathf.Approximately(PlayerInput.Vertical, 0))
        {
            if (PlayerInput.Horizontal > 0)
            {
                _movementController.MoveLeft();
            }

            if (PlayerInput.Horizontal < 0)
            {
                _movementController.MoveRight();
            }
        }

        if (Mathf.Approximately(PlayerInput.Horizontal, 0))
        {
            if (PlayerInput.Vertical > 0)
            {
                _movementController.MoveBack();
            }

            if (PlayerInput.Vertical < 0)
            {
                _movementController.MoveForward();
            }
        }
    }
}
