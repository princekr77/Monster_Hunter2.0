using UnityEngine;

public class PlayerAimShoot : MonoBehaviour
{
    [Header("References")]
    public Animator animator;   // Drag your player’s Animator here in Inspector

    private bool isAiming = false;

    void Update()
    {
        if (animator == null) return;

        // Keep animator updated
        animator.SetBool("Aiming", isAiming);
    }

    // 🚀 Call this when button pressed (hold)
    public void OnAimButtonDown()
    {
        isAiming = true;
    }

    // 🚀 Call this when button released
    public void OnAimButtonUp()
    {
        isAiming = false;
        animator.SetTrigger("Shooting");
    }
}
