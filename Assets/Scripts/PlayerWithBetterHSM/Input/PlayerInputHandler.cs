using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool RollInput { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.1f;
    private float jumpInputStartTime,
        dashInputStartTime;

    private void Start()
    {
        RawMovementInput = Vector2.zero;
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormalizedInputX = (int)(RawMovementInput.x * Vector2.right).normalized.x;
        NormalizedInputY = (int)(RawMovementInput.y * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RollInput = true;
        }
    }

    private void Update()
    {
        if (Time.time > jumpInputStartTime + inputHoldTime)
        {
            UseJumpInput();
        }
        if (Time.time > dashInputStartTime + inputHoldTime)
        {
            UseDashInput();
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void UseDashInput() => DashInput = false;

    public void UseRollInput() => RollInput = false;
}
