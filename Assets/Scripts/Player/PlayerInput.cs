using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Movement { get; private set; }

    public bool DashPressed { get; private set; }

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Combat.Dash.performed += OnDashPerformed;
    }

    private void OnDisable()
    {
        controls.Combat.Dash.performed -= OnDashPerformed;

        controls.Disable();
    }

    private void Update()
    {
        Movement = controls.Movement.Move.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        DashPressed = false;
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        DashPressed = true;
    }
}