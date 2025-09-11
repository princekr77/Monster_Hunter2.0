using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        currentHealth = maxHealth;

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

        FindObjectOfType<AudioManager>().HurtAudio();

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

        animator.SetBool("Dead", true);
        Debug.Log("☠️ Player Died!");
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Trigger Death Animation
        if (animator != null)
        {
           // animator.SetTrigger("Die");
            //animator.SetBool("isDead", true); //depending on how you set up Animator
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
