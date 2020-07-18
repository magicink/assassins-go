using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;
    private bool _inputEnabled;

    public float Horizontal => _horizontal;
    public float Vertical => _vertical;

    public bool InputEnabled
    {
        get => _inputEnabled;
        set => _inputEnabled = value;
    }

    public void GetKeyInput()
    {
        if (InputEnabled)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
        }
    }
}
