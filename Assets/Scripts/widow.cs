using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class widow : MonoBehaviour
{
    public int HP = 100;
    public Slider healthBar;
    public Animator animator;
    private bool isDead = false;

    [Header("Attack Settings")]
    public int attackDamage = 10;
    public float attackRate = 1.5f; // seconds between attacks
    private float nextAttackTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                // Widow attacks player
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                }

                if (animator != null)
                    animator.SetTrigger("isAttacking"); // play attack animation

                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // prevent multiple deaths

        HP -= damageAmount;
        healthBar.gameObject.SetActive(true); // show healthbar on hit

        if (HP <= 0)
        {
            Die();
        }
        else
        {
            if (animator != null)
                animator.SetTrigger("damage");

        }
    }

    private void Die()
    {
        isDead = true;
        HP = 0;
        healthBar.value = HP;
        FindObjectOfType<AudioManager>().KillAudio();

        if (animator != null)
            animator.SetTrigger("die");

        GetComponent<Collider>().enabled = false;

        StartCoroutine(DestroyAfterDelay(2f)); // delay to let animation play
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void Start()
    {
        healthBar.gameObject.SetActive(false); // hidden at start
        healthBar.maxValue = HP;
        healthBar.value = HP;
    }

    void Update()
    {
        healthBar.value = HP;
    }
}
