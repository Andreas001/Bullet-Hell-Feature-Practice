using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
    public int normalizeInputX { get; private set; }
    public int normalizeInputY { get; private set; }
    public bool fireInput { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context) {
        Vector2 rawMovementInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(rawMovementInput.x) > 0.5f) {
            normalizeInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        }
        else {
            normalizeInputX = 0;
        }

        if (Mathf.Abs(rawMovementInput.y) > 0.5f) {
            normalizeInputY = (int)(rawMovementInput * Vector2.up).normalized.y;
        }
        else {
            normalizeInputY = 0;
        }
    }

    public void OnFireInput(InputAction.CallbackContext context) {
        if (context.started) {
            fireInput = true;
        }

        if (context.canceled) {
            fireInput = false;
        }
    }
}
