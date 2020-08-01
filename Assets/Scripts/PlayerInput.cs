using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;

    public float Horizontal => _horizontal;
    public float Vertical => _vertical;

    public bool InputEnabled { get; set; }

    public void GetKeyInput()
    {
        if (!InputEnabled) return;
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }
}
