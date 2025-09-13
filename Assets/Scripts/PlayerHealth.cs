using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;   // for PlayerInput
using StarterAssets;             // for ThirdPersonController

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    [Header("UI")]
    public Slider healthBar; // Assign in Inspector
    public GameObject gameOverUI;

    [Header("Animation")]
    public Animator animator; // Assign in Inspector (Player Animator)

    // References
    private PlayerInput playerInput;
    private ThirdPersonController controller;

    void Start()
    {
        currentHealth = maxHealth;

        // Assign components
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<ThirdPersonController>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    // Called when Widow attacks
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Play hurt sound if AudioManager exists
        var audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
            audioManager.HurtAudio();

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // prevent multiple calls
        isDead = true;

        Debug.Log("☠️ Player Died!");
        FindObjectOfType<AudioManager>().GameOverAudio();

        // Show Game Over UI
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable movement and input
        if (playerInput != null) playerInput.enabled = false;
        if (controller != null) controller.enabled = false;

        // Trigger Death Animation
        if (animator != null)
        {
            animator.SetBool("Dead", true); // make sure you have "Dead" bool in Animator
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
