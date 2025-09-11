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

    // Aim button
    public void VirtualAimInput(bool virtualAimState)
    {
        inputs.AimInput(virtualAimState);
    }

    // Shoot button
    public void VirtualShootInput(bool virtualShootState)
    {
        inputs.ShootInput(virtualShootState);
    }
}
