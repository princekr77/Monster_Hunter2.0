using UnityEngine;

public class UICanvasControllerInput : MonoBehaviour
{
    [Header("Output to Starter Assets")]
    public StarterAssets.StarterAssetsInputs inputs;

    // Movement from joystick
    public void VirtualMoveInput(Vector2 virtualMoveDirection)
    {
        inputs.MoveInput(virtualMoveDirection);
    }

    // Camera look from touch
    public void VirtualLookInput(Vector2 virtualLookDirection)
    {
        inputs.LookInput(virtualLookDirection);
    }

    // Jump button
    public void VirtualJumpInput(bool virtualJumpState)
    {
        inputs.JumpInput(virtualJumpState);
    }

    // Sprint button
    public void VirtualSprintInput(bool virtualSprintState)
    {
        inputs.SprintInput(virtualSprintState);
    }

    // Android/iOS: Aim + auto shoot on release
    public void VirtualAimShootInput(bool pressed)
    {
#if UNITY_ANDROID || UNITY_IOS
        if (pressed)
        {
            // When pressed → start aiming
            inputs.AimInput(true);
            inputs.ShootInput(false);
        }
        else
        {
            // When released → fire once
            inputs.AimInput(false);
            inputs.ShootInput(true);
        }
#endif
    }

    // Separate Aim (for PC or if needed on mobile)
    public void VirtualAimInput(bool pressed)
    {
        inputs.AimInput(pressed);
    }

    // Separate Shoot (for PC or if needed on mobile)
    public void VirtualShootInput(bool pressed)
    {
        inputs.ShootInput(pressed);
    }
}
