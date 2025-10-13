using UnityEngine;
using UnityEngine.EventSystems; // Required for IPointerDownHandler and IPointerUpHandler
using StarterAssets;


public class UICanvasControllerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Output to Starter Assets")]
    public StarterAssets.StarterAssetsInputs inputs;

    [Header("References")]
    public Animator animator;   // Drag Player Animator here in Inspector

    //private bool isAiming = false;

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

    // When button pressed down → Aim
    public void OnPointerDown(PointerEventData eventData)
    {
        //isAiming = true;
        inputs.AimInput(true);

        if (animator != null)
        {
            animator.SetBool("Aiming", true);
        }
    }

    // When button released → Shoot
    public void OnPointerUp(PointerEventData eventData)
    {
        //isAiming = false;
        inputs.AimInput(false);

        // Trigger shooting
        inputs.ShootInput(true);
        if (animator != null)
        {
            animator.SetBool("Shooting", true);
        }

        var controller = inputs.GetComponent<ThirdPersonController>();
        if (controller != null)
        {
            controller.Shoot();
        }

        // Reset shoot after small delay so it doesn’t stay true forever
        inputs.ShootInput(false);
        if (animator != null)
        {
            animator.SetBool("Shooting", false);
        }
    }
}
